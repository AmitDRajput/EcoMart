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
    public partial class UclSaleListProduct : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;   
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        public static string FromDt = ""; // [06.02.2017]
        public static string ToDt = "";   // [06.02.2017]
        private List<DataGridViewRow> rowCollection;
        private int selectedRowCount;
        #endregion

        # region Constructor
        public UclSaleListProduct()
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
        #endregion Constructor

        #region IOverview Members       

        public override void ShowOverview()
        {
            try
            {               
                _BindingSource = new DataTable();
                _SaleList = new SaleList(); 
                headerLabel1.Text = "SALE PRODUCT";               
                ClearControls();                             
                HidepnlGO();                                     
                AddToolTip();               
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
                mcbProduct.SelectedID = ID;
                ShowReportGrid(ID);

            }
        }
        private void ShowReportGrid(string productID)
        {
            InitializeReportGrid();
            ShowpnlGO();
            FillReportGrid(productID);
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
                int totalrows = dgvReportList.Rows.Count;
                totalrows =  CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
               // totalrows += 7; // for first page heading
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead(FromDt, ToDt);
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_ID"].Value != null)
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
                            PrintHead(FromDt, ToDt);
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                      //  row = new PrintRow("", 0, 0, PrintFont);
                        double mamt = 0;

                        if (dr.Cells["Col_VoucherType"].Value == null && dr.Cells["Col_Batch"].Value != null)
                        {


                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 13;
                            PrintRowCount += 1;
                            if (dr.Cells["Col_Name"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Name"].Value.ToString().PadRight(30), PrintRowPixel, 180, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Batch"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 430, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Quantity"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 530, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Amount"].Value != null)
                            {

                                mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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
                            if (dr.Cells["Col_VoucherType"].Value == null && dr.Cells["Col_Name"].Value != null && dr.Cells["Col_Batch"].Value == null)
                            {
                                row = new PrintRow(dr.Cells["Col_Name"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            else
                            {
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
                                    row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 80, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_VoucherType"].Value != null && dr.Cells["Col_Name"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Name"].Value.ToString().PadRight(30), PrintRowPixel, 180, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                             
                                if (dr.Cells["Col_Batch"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 430, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Quantity"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 530, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Rate"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_Rate"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(570.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Amount"].Value != null)
                                {

                                    mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                            }
                        }
                        
                    }
                }
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
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {

                if (dr.Cells["Col_Batch"].Value != null && dr.Cells["Col_Batch"].Value.ToString() == "Total")
                    totrows += 2;
            }
            return totrows;
        }

        private int PrintHead(string FromDate, string ToDate)
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(_MFromDate), General.GetDateInDateFormat(_MToDate));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 50, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 90, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 230, PrintFont);
                PrintBill.Rows.Add(row);               
                row = new PrintRow("Batch", PrintRowPixel, 430, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 530, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("S.Rate", PrintRowPixel, 600, PrintFont);
                PrintBill.Rows.Add(row);               
                row = new PrintRow("Amount", PrintRowPixel, 670, PrintFont);
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

        private int PrintFooter(int PrintRowPixel) // [06.02.2017]
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

        # region Other Private methods

        public void ClearControls()
        {
            try
            {
                cbSelectAll.Checked = false;
                fromDate1.Value = DateTime.Now;
                toDate1.Value = DateTime.Now;
                lblFooterMessage.Text = "";
                mcbProduct.SelectedID = "";
                cbScheduleH1.Checked = false;
                FillMultiSelectionGrid();
                FillProductCombo(); 
                ConstructSelectedGridColumns();                
                rowCollection = new List<DataGridViewRow>();
                dgvMultiSelection.Columns["Col_ID"].Visible = false;
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;
                DataTable dtable = new DataTable();    
                InitializeReportGrid();
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
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Sub Type";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";              
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Party";
                column.Width = 300;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "Address";
                column.HeaderText = "Address";
                column.Width = 300;
               // column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Rate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;               
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 150;
              //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;         
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccCode";
                column.DataPropertyName = "AccCode";
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
                //columnCheck.DataPropertyName = "TAG";
                columnCheck.HeaderText = "Check";
                column.ValueType = typeof(bool);
              //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                columnCheck.Width = 50;
                dgvMultiSelection.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              //  column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
              //  column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //    column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.ValueType = typeof(string);
             //   column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructSelectedGridColumns()
        {
            try
            {
                dgvSelected.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
             //   column.DataPropertyName = "AccountID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
           //     column.DataPropertyName = "AccName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";               
                column.HeaderText = "UOM";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //    column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.HeaderText = "Pack";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
             //   column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.HeaderText = "Comp";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
              //  column.Width = 200;
                dgvSelected.Columns.Add(column);


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
        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
        }
        public void HidepnlGO()
        {

            ConstructMultiSelectionGridColumns();
            dgvMultiSelection.Columns["Col_ID"].Visible = false;
            dgvSelected.Rows.Clear();
            dgvSelected.Visible = false;
            cbSelectAll.Checked = false;          
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            ClearMultiselectionFilter();
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
        private void RefreshSelectedRowCounter()
        {
            selectedRowCount = dgvSelected.Rows.Count;
            txtNoofSearches.Text = selectedRowCount.ToString("#0");
        }
        private void BindReportGrid(string ID, string product, string UOM, string Pack, string comp)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                currentdr.Cells["Col_ID"].Value = ID;
                currentdr.Cells["Col_Name"].Value = product.Trim() + " " + UOM + " " + Pack + " " + comp;

                double mtotamount = 0;
                int mtotquantity = 0;             

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["ProductID"].ToString() == ID && Convert.ToInt32(dr["VoucherDate"].ToString()) >= Convert.ToInt32(_MFromDate) && Convert.ToInt32(dr["VoucherDate"].ToString()) <= Convert.ToInt32(_MToDate))
                    {

                        double mamt = 0;

                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["MasterSaleID"].ToString();
                        currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_Name"].Value = dr["PatientName"].ToString();
                        currentdr.Cells["Col_Address"].Value = dr["Address"].ToString();
                        currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();                      
                        currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();                      
                        mamt = Convert.ToDouble(dr["SaleRate"].ToString());
                        currentdr.Cells["Col_Rate"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["Amount"].ToString());
                        currentdr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        mtotamount += Convert.ToDouble(dr["Amount"].ToString());
                        mtotquantity += Convert.ToInt32(dr["Quantity"].ToString());                     
                    }
                }
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_ID"].Value = ID;
             //   currentdr.Cells["Col_Name"].Value = product.Trim() + " " + UOM + " " + Pack + " " + comp;
                currentdr.Cells["Col_Batch"].Value = "Total";
                currentdr.Cells["Col_Quantity"].Value = mtotquantity.ToString();              
                currentdr.Cells["Col_Amount"].Value = mtotamount.ToString("#0.00");
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }

        private void CheckFiltertxtSearch(string txtString)
        {

            try
            {
                _BindingSourceMultiSelection.DefaultView.RowFilter = "ProdName like '" + txtString + "%'";
            }
            catch (Exception ex) { Log.WriteException(ex); }
        }

        private void ClearMultiselectionFilter()
        {

            try
            {
                _BindingSourceMultiSelection.DefaultView.RowFilter = "";
            }
            catch (Exception ex) { Log.WriteException(ex); }
        } 
      
        private void FillMultiSelectionGrid()
        {

            try
            {
                selectedRowCount = 0;
                RefreshSelectedRowCounter();
                dgvMultiSelection.DataSource = null;
                ConstructMultiSelectionGridColumns();
                ConstructSelectedGridColumns();
                dgvMultiSelection.Columns["Col_ID"].Visible = false;
                FillMultiSelectionData();
               
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
                DataTable dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetOverviewDataForReports();
                _BindingSourceMultiSelection = dtable;
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;           
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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

        private void btnViewListClick()
        {
            dgvSelected.Sort(dgvSelected.Columns["Col_Name"], ListSortDirection.Ascending);
            if (dgvSelected.Visible == false)
            {
                dgvSelected.Visible = true;
                dgvMultiSelection.Enabled = false;
                btnViewList.Text = "Close";
            }
            else
            {
                dgvSelected.Visible = false;
                dgvMultiSelection.Enabled = true;
                btnViewList.Text = "View";
            }
        }    

        private void GetSaleDataProductwise(string productID)
        {
            DataTable dtable = new DataTable();
            dtable = _SaleList.GetSaleDataForGivenProduct(productID);          
            _BindingSource = dtable;
        }

     

        private void AddIndgvSelectedGrid(DataGridViewRow drow)
        {
            bool iffound = false;
            foreach (DataGridViewRow drowselected in dgvSelected.Rows)
            {
                if (drowselected.Cells["Col_ID"].Value.ToString() == drow.Cells["Col_ID"].Value.ToString())
                {
                    iffound = true;
                    break;
                }
            }
            if (!iffound)
            {

                int selectedrowindex = dgvSelected.Rows.Add();
                dgvSelected.Rows[selectedrowindex].Cells["Col_ID"].Value = drow.Cells["Col_ID"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Name"].Value = drow.Cells["Col_Name"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_UOM"].Value = drow.Cells["Col_UOM"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Pack"].Value = drow.Cells["Col_Pack"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Comp"].Value = drow.Cells["Col_Comp"].Value.ToString();
            }

        }

        private void RemoveFromdgvSelectedGrid(DataGridViewRow drow)
        {
            foreach (DataGridViewRow drowselected in dgvSelected.Rows)
            {
                if (drowselected.Cells["Col_ID"].Value.ToString() == drow.Cells["Col_ID"].Value.ToString())
                {
                    dgvSelected.Rows.Remove(drowselected);
                    break;
                }
            }


        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }
        #endregion Other Private Methods

        # region Events   
    
        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void AddOptionalColumns()
        {
            dgvReportList.OptionalColumnNames.Add("Col_Amount");
            dgvReportList.OptionalColumnNames.Add("Col_Rate");
        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                InitializeReportGrid();
                AddOptionalColumns();

                _MFromDate = FromDt = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = ToDt = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string mproductID = "";
                    FromDt = General.GetDateInDateFormat(FromDt);
                    ToDt = General.GetDateInDateFormat(ToDt);
                    ShowpnlGO();
                    rowCollection = new List<DataGridViewRow>();
                    if (dgvSelected.Rows.Count > 0)
                    {
                        dgvSelected.Sort(dgvSelected.Columns["Col_Name"], ListSortDirection.Ascending);
                        foreach (DataGridViewRow selectedrow in dgvSelected.Rows)
                        {
                            rowCollection.Add(selectedrow);
                        }
                    }

                    if (rowCollection.Count > 0)
                    {
                        if (dgvReportList.Rows.Count > 0)
                            dgvReportList.Rows.Clear();
                        mproductID = "";
                        foreach (DataGridViewRow row in rowCollection)
                        {
                            mproductID = "";
                            if (row.Cells["Col_ID"].Value != null)
                                mproductID = row.Cells["Col_ID"].Value.ToString();
                            if (mproductID != "")
                            {
                                GetSaleDataProductwise(mproductID);
                                if (_BindingSource.Rows.Count > 0)
                                {
                                    BindReportGrid(row.Cells["Col_ID"].Value.ToString(), row.Cells["Col_Name"].Value.ToString(), row.Cells["Col_UOM"].Value.ToString(),
                                    row.Cells["Col_Pack"].Value.ToString(), row.Cells["Col_Comp"].Value.ToString());
                                }
                            }
                        }
                        this.Cursor = Cursors.Default;
                        PrintReportHead = "Sales Report [Product]"; // From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                        PrintReportHead2 = "";
                        NoofRows();
                        dgvReportList.Focus();
                    }
                    else if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                    {
                        mproductID = FillReportGrid(mproductID);
                    }

                }
                else
                    lblFooterMessage.Text = "Please Check Date...";

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private string FillReportGrid(string mproductID)
        {
            mproductID = mcbProduct.SelectedID.ToString();
            GetSaleDataProductwise(mproductID);
            if (_BindingSource.Rows.Count > 0)
            {
                BindReportGrid(mcbProduct.SeletedItem.ItemData[0].ToString(), mcbProduct.SeletedItem.ItemData[1].ToString(), mcbProduct.SeletedItem.ItemData[2].ToString(),
                mcbProduct.SeletedItem.ItemData[3].ToString(), mcbProduct.SeletedItem.ItemData[4].ToString());
            }
            this.Cursor = Cursors.Default;
            PrintReportHead = "Sales Report [Product]"; // From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
            PrintReportHead2 = "";
            NoofRows();
            dgvReportList.Focus();
            return mproductID;
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int mlen = 0;
                mlen = txtSearch.Text.ToString().Trim().Length;
                if ((txtSearch.Text != null && txtSearch.Text != "") || cbScheduleH1.Checked == true)
                {
                    if (cbSelectAll.Checked == true)
                    {
                        if (dgvMultiSelection.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                            {

                                if (drow.Cells["Col_Name"].Value != null &&  txtSearch.Text.ToString().ToUpper() == drow.Cells["Col_Name"].Value.ToString().Trim().Substring(0, mlen))
                                {
                                    drow.Cells["Col_Check"].Value = cbSelectAll.Checked;
                                }
                            }
                        }
                        txtSearch.Text = "";
                        cbSelectAll.Checked = false;
                    }

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void ToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbProduct.Focus();
        }

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            string voutype = "";
            string vousubtype = "";
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    voutype = dgvReportList.SelectedRow.Cells[1].Value.ToString();
                    vousubtype = dgvReportList.SelectedRow.Cells["Col_VoucherSubType"].Value.ToString();
                    //if (vousubtype == FixAccounts.SubTypeForPatientSale)
                    //    ViewControl = new UclPatientSale();
                    //else if (vousubtype == FixAccounts.SubTypeForHospitalSale)
                    //    ViewControl = new UclHospitalSale();
                    //else if (vousubtype == FixAccounts.SubTypeForInstitutionalSale)
                    //    ViewControl = new UclInstitutionalSale();
                    if (vousubtype == FixAccounts.SubTypeForRegularSale)
                        ViewControl = new UclDistributorSale("R");
                    ShowViewForm(selectedID, ViewMode.Current);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void dgvMultiSelection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMultiSelection.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvMultiSelection_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvMultiSelection.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if (Convert.ToBoolean(dgvMultiSelection.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true)
                    {
                        selectedRowCount++;
                        rowCollection.Add(dgvMultiSelection.Rows[e.RowIndex]);
                        AddIndgvSelectedGrid(dgvMultiSelection.Rows[e.RowIndex]);
                    }
                    else
                    {
                        selectedRowCount--;
                        RemoveFromdgvSelectedGrid(dgvMultiSelection.Rows[e.RowIndex]);
                    }
                    RefreshSelectedRowCounter();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearch.Text != null && txtSearch.Text.ToString() != "")
            {
                CheckFiltertxtSearch(txtSearch.Text.ToString().Trim());
                cbSelectAll.Visible = true;
                cbSelectAll.Checked = false;
                dgvMultiSelection.Focus();
            }
        }

        private void btnViewList_KeyDown(object sender, KeyEventArgs e)
        {
            btnViewListClick();
        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            btnViewListClick();
        }
        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbProduct.SelectedID == null || mcbProduct.SelectedID == "")
                txtSearch.Focus();
            else
                btnOKMultiSelectionClick();
        }
        #endregion  Events 

        #region ToolTip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(txtSearch , "Type First Few characters of Product Name and Press Enter");
                ttToolTip.SetToolTip(cbSelectAll, "Check to Select All Products in the List and Click OK");
                ttToolTip.SetToolTip(btnViewList, "Click to View List of Selected Products and Click Again to Close the List");
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
                ttToolTip.SetToolTip(dgvReportList, "Home key for Selection, Double Click to View");
                ttToolTip.SetToolTip(pnlTop, "Home key for Selection, Double Click to View");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion  ToolTip

        private void cbScheduleH1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbScheduleH1.Checked)
            {               
                FillMultiselectionDataForH1();
            }
            else
            {
                mcbProduct.UserControlToShow = new UclProduct();
                DataTable dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetOverviewData();
                mcbProduct.FillData(dtable);
                FillMultiSelectionData();
            }
        }

        private DataTable FillMultiselectionDataForH1()
        {
            DataTable dtable = new DataTable();
            dtable = _SaleList.FillScheduleH1ProductList();
           // dtable = EcoMartCache.GetProductData();
            _BindingSourceMultiSelection = dtable;
            ConstructMultiSelectionGridColumns();
            dgvMultiSelection.Columns["Col_ID"].Visible = false;           
            dgvMultiSelection.DataSource = _BindingSourceMultiSelection;
            dgvMultiSelection.Columns["Col_ID"].Visible = false;      
            return dtable;
        }
       
    }
}
