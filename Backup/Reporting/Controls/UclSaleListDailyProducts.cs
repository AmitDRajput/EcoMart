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

namespace PharmaSYSRetailPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSaleListDailyProducts : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;       
        private double _MTotalAmount;
        #endregion Declaration

        # region Constructor
        public UclSaleListDailyProducts()
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
                headerLabel1.Text = "SALE DAILY PRODUCTS";
                ClearControls();               
                AddToolTip();
                HidepnlGO();                
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
            fromDate1.Focus();
        }
        #endregion

        # region IReport Members

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
                PrintReportHead = "Sale Daily Products Date : " + General.GetDateInDateFormat(_MFromDate); 
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 7, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
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
                            PrintHead();
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;

                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 30, PrintFont);
                        PrintBill.Rows.Add(row);

                        if (dr.Cells["Col_ProdName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProdName"].Value.ToString().PadRight(30), PrintRowPixel, 70, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_UOM"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_UOM"].Value.ToString().PadRight(30), PrintRowPixel, 200, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Pack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Pack"].Value.ToString().PadRight(30), PrintRowPixel, 230, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        //if (dr.Cells["Col_Comp"].Value != null)
                        //{
                        //    row = new PrintRow(dr.Cells["Col_Comp"].Value.ToString().PadRight(30), PrintRowPixel, 440, PrintFont);
                        //    PrintBill.Rows.Add(row);
                        //}
                        if (dr.Cells["Col_Batch"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 275, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Expiry"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Expiry"].Value.ToString(), PrintRowPixel, 340, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Rate"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Rate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(365.00 + ((12.00 - Convert.ToDouble(mlen)) * 3.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(20), PrintRowPixel, 440, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_Quantity"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 640, PrintFont);
                            PrintBill.Rows.Add(row);
                        }                      
                       
                        if (dr.Cells["Col_Amount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(660.00 + ((12.00 - Convert.ToDouble(mlen)) * 3.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_User"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_User"].Value.ToString().PadLeft(6), PrintRowPixel, 720, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ClosingStock"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ClosingStock"].Value.ToString().PadLeft(6), PrintRowPixel, 770, PrintFont);
                            PrintBill.Rows.Add(row);
                        }     
                    }
                }
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                mlen = (_MTotalAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(660.00 + ((12.00 - Convert.ToDouble(mlen)) * 3.5));
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
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

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;
                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 30, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Product", PrintRowPixel, 70, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("UOM", PrintRowPixel, 200, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 230, PrintFont);
                PrintBill.Rows.Add(row);
                //row = new PrintRow("Comp", PrintRowPixel, 260, PrintFont);
                //PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 275, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("EXP", PrintRowPixel, 340, PrintFont);
                PrintBill.Rows.Add(row);               
                row = new PrintRow("SRate", PrintRowPixel, 385, PrintFont);
                PrintBill.Rows.Add(row); 
                row = new PrintRow("Party", PrintRowPixel, 440, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 640, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 690, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("User", PrintRowPixel, 720, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CL STK", PrintRowPixel, 770, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow(FixAccounts.DashLine80Condenced, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 7;
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
            _MFromDate = DateTime.Now.Date.ToString("yyyyMMdd");
            fromDate1.Value = DateTime.Now;
            
         

        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            FormatReportGrid();
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;           
            tsbtnPrint.Enabled = false;
            ViewFromDate.Visible = false;
            InitializeDates();
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;           
            tsbtnPrint.Enabled = true;
            ViewFromDate.Visible = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
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
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "TYPE";
                column.Width = 60;
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
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Visible = false;
                column.Width = 60;
                dgvReportList.Columns.Add(column);                

                
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Rate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);                

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 80;                
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_User";
                column.DataPropertyName = "UserName";
                column.HeaderText = "User";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "CL STK";
                column.Width = 60;
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
                FormatReportGrid();
                dgvReportList.Bind();
              
                _MTotalAmount = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null)
                        _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                }              
                txtReportTotalAmount.Text = _MTotalAmount.ToString("#0.00");
                NoofRows();
                dgvReportList.Focus();
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
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");  
                dtable = _SaleList.GetSaleDataForDailyProducts(_MFromDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FormatReportGrid()
        {
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DoubleColumnNames.Add("Col_Rate");
        }

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }
        #endregion

        # region Events      
        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }
        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelectionClick()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");              
                FillReportGrid();
                ShowpnlGO();
                this.Cursor = Cursors.Default;
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
                    if (vousubtype == FixAccounts.SubTypeForPatientSale)
                        ViewControl = new UclPatientSale();
                    else if (vousubtype == FixAccounts.SubTypeForHospitalSale)
                        ViewControl = new UclHospitalSale();
                    else if (vousubtype == FixAccounts.SubTypeForInstitutionalSale)
                        ViewControl = new UclInstitutionalSale();
                    else if (vousubtype == FixAccounts.SubTypeForDebtorSale)
                        ViewControl = new UclDebtorSale();
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

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = F10");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home Key to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion

    }
}
