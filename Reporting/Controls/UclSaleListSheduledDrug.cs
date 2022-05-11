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
    public partial class UclSaleListSheduledDrug : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        #endregion

        # region Constructor
        public UclSaleListSheduledDrug()
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
                headerLabel1.Text = "SALE SCHEDULED DRUG";            
                ClearControls(); 
                AddToolTip();
               
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
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead(_MFromDate, _MToDate);
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    double mamt = 0;
                    if (dr.Cells["Col_ID"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel += 17;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintPageNumber += 1;
                            PrintHead(_MFromDate, _MToDate);
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        if (dr.Cells["Col_ProdName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProdName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_UOM"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_UOM"].Value.ToString().PadRight(30), PrintRowPixel, 140, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Pack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Pack"].Value.ToString().PadRight(30), PrintRowPixel, 170, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Comp"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Comp"].Value.ToString().PadRight(30), PrintRowPixel, 200, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Batch"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 230, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Quantity"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 280, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Rate"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Rate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(300.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 370, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 400, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 450, PrintFont);
                        PrintBill.Rows.Add(row);       
                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 500, PrintFont);
                        PrintBill.Rows.Add(row);
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
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                mlen = (_MTotalAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintFooter(PrintRowPixel);
                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHead(string _MFromDate, string _MToDate)
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(_MFromDate), General.GetDateInDateFormat(_MToDate));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Product", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("UOM", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 170, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Comp", PrintRowPixel, 200, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 230, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 280, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SRate", PrintRowPixel, 340, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Type", PrintRowPixel, 370, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 400, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel,450, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 500, PrintFont);
                PrintBill.Rows.Add(row);
                
                row = new PrintRow("Amount", PrintRowPixel, 680, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }
        private int PrintFooter(int PrintRowPixel) // [07.02.2017]
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
        public void ClearControls()
        {
            try
            {
                string startdate = General.GetDateInDateFormat(General.ShopDetail.Shopsy);               
                lblFooterMessage.Text = "";
                ConstructReportColumns();
                fromDate1.Value = DateTime.Now;
                toDate1.Value = DateTime.Now;
                DataTable dtable = new DataTable();
                dgvReportList.DataSource = dtable;
                dgvReportList.Bind();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }  
        }
            
        #endregion

        # region Other Private methods
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
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "Product";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 60;
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
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Party";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Rate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccCode";
                column.DataPropertyName = "AccCode";
                column.Visible = false;
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
                dgvReportList.DataSource = _BindingSource;              
                dgvReportList.DateColumnNames.Add("Col_VoucherDate");
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
                dgvReportList.Bind();
                _MTotalAmount = 0;             
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null)
                        _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());               
                }               
                txtReportTotalAmount.Text = _MTotalAmount.ToString("#0.00");
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
                ConstructReportColumns();
                DataTable dtable  = new DataTable();
                dgvReportList.DataSource = dtable;
                dgvReportList.Bind();            
                dtable = _SaleList.GetSaleDataForScheduledProducts(_MFromDate, _MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }    
        #endregion

        # region Events  

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            string macccode = "";
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    
                    if (dgvReportList.SelectedRow.Cells["Col_AccCode"].Value != null && dgvReportList.SelectedRow.Cells["Col_AccCode"].Value.ToString() != "")
                        macccode = dgvReportList.SelectedRow.Cells["Col_AccCode"].Value.ToString();

                    // if (macccode != FixAccounts.AccCodeForDebtor)
                    //    ViewControl = new UclPatientSale();
                    //else
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

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelectionClick()
        {
            try
            {
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                this.Cursor = Cursors.WaitCursor;
                FillReportGrid();
                this.Cursor = Cursors.Default;
                PrintReportHead = "Sale Scheduled Drug"; //  From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                PrintReportHead2 = "";
                dgvReportList.Focus();
                pnlMultiSelection1.SendToBack();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {               
                
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion    
    }
}
