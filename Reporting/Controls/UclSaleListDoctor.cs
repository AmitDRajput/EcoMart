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
    public partial class UclSaleListDoctor : ReportBaseControl
    {
        #region Declaration
        private SsStock _SsStock;
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;      
        #endregion

        # region Constructor
        public UclSaleListDoctor()
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
                _SaleList = new SaleList();
                _SsStock = new SsStock();
                headerLabel1.Text = "SALE DOCTOR";
                ClearControls();
                FillReportGrid();
                FillDoctorCombo();
                HidepnlGO();
                AddToolTip(); 
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void ShowReport(string docID, string Compid, string FromDate, string ToDate)
        {
            base.ShowReport(docID,Compid, FromDate, ToDate);
            if (Compid != null && Compid != "")
            {
                _MFromDate = FromDate;
                _MToDate = ToDate;
                mcbCompany.SelectedID = Compid;
                ShowReportGrid(docID, Compid, FromDate, ToDate);              


            }
        }
        private void ShowReportGrid(string docID, string CompID, string fromDate, string toDate)
        {
            InitializeReportGrid();
            ShowpnlGO();
            FillReportGridShowReport(docID, CompID, fromDate, toDate);
        }
        public override void SetFocus()
        {
            base.SetFocus();
            FromDate.Focus();

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
                            PrintHead(_MFromDate,_MToDate);
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
                            row = new PrintRow(dr.Cells["Col_Comp"].Value.ToString().PadRight(30), PrintRowPixel, 210, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Batch"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 240, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Quantity"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 310, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Rate"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Rate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(300.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix+20, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 400, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 430, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 480, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 540, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_Amount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix+30, PrintFont);
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
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix+30, PrintFont);
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

        private int PrintHead(string _MFromDate,string _MToDate)
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
                row = new PrintRow("Comp", PrintRowPixel, 205, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 240, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 310, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SRate", PrintRowPixel, 350, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Type", PrintRowPixel, 390, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 425, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 480, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Amount", PrintRowPixel, 710, PrintFont);
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
        #endregion IReport Memebers

        # region Other Private methods

        public void ClearControls()
        {
            try
            {
                FromDate.Value = DateTime.Now;
                ToDate.Value = DateTime.Now;        
                lblFooterMessage.Text = "";
                txtReportTotalAmount.Text = "";              
                InitializeReportGrid();
                mcbDoctor.SelectedID = "";
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SUB";
                column.Visible = false;
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.HeaderText = "Qty";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Rate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;              
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.Visible = false;
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
        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DoubleColumnNames.Add("Col_Rate");
        }
        public void HidepnlGO()
        {                        
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            pnlMultiSelection.Visible = true;
            FromDate.Focus();
        }

        public void ShowpnlGO()
        {
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }       
            pnlMultiSelection.Visible = false;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
        }
     
        private void FillReportGrid()
        {
            try
            {
                FillReportData();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
                _MTotalAmount = 0;             
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
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
        private void FillReportGridShowReport(string docID, string CompID, string fromDate, string toDate)
        {
            try
            {
                FillReportDataShowReport(docID, CompID, fromDate, toDate);
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
                _MTotalAmount = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                }

                txtReportTotalAmount.Text = _MTotalAmount.ToString("#0.00");
                dgvReportList.Columns[0].Visible = false;
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
                if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                    dtable = _SaleList.GetSaleDataForDoctor(mcbDoctor.SelectedID, _MFromDate,_MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillReportDataShowReport(string docID, string CompID, string fromDate, string toDate )
        {
            try
            {
                DataTable dtable = new DataTable();
               
                    dtable = _SaleList.GetSaleDataForDoctorShowReport(docID, CompID, fromDate, toDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[4] { "DocID", "DocName", "DocAddress", "DocShortNameAddress" };
                mcbDoctor.ColumnWidth = new string[4] { "0", "200", "300", "0" };
                mcbDoctor.ValueColumnNo = 0;
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _Doctor = new Doctor();
                DataTable dtabled = _Doctor.GetSSDoctorsList();
                mcbDoctor.FillData(dtabled);
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
        #endregion

        # region Events
        private void FromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ToDate.Focus();
        }

        private void ToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
            if (e.KeyCode == Keys.Up)
                FromDate.Focus();
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
                
                InitializeReportGrid();
                FormatReportGrid();
                _MFromDate = FromDate.Value.Date.ToString("yyyyMMdd");
                _MToDate = ToDate.Value.Date.ToString("yyyyMMdd");
                 retValue = General.CheckDates(_MFromDate, _MToDate);
                 if (retValue)
                 {
                     this.Cursor = Cursors.WaitCursor;
                     ShowpnlGO();
                     txtViewText.Text = mcbDoctor.SeletedItem.ItemData[1].ToString();
                     FillReportGrid();
                    PrintReportHead = "Sale Doctor"; //  From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                     PrintReportHead2 = "[" + mcbDoctor.SeletedItem.ItemData[1].ToString() + "]";
                     this.Cursor = Cursors.Default;
                     dgvReportList.Focus();
                 }
                

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void mcbDoctor_SeletectIndexChanged(object sender, EventArgs e)
        {
            //if (mcbDoctor.SelectedID != null)
                //btnOKMultiSelectionClick();
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

        #endregion Events

        #region ToolTip
        private void AddToolTip()
        {
            try
            {               
                ttSaleDoctor.SetToolTip(btnOKMultiSelection1, "Click to See Report = F10");
                ttSaleDoctor.SetToolTip(pnlMultiSelection, "F12 to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion ToolTip

    
      
    }
}
