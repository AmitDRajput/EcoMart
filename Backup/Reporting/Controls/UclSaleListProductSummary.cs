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
    public partial class UclSaleListProductSummary : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        #endregion

        # region Constructor
        public UclSaleListProductSummary()
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
                headerLabel1.Text = "PRODUCT SUMMARY";
                fromDate1.Value = DateTime.Now;
                toDate1.Value = DateTime.Now;
                ClearControls();               
                AddToolTip();
           
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
                 PrintReportHead = "Sale Product Summary  From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                 PrintReportHead2 = "";
                 PrintBill.Rows.Clear();
                 PrintFont = new Font("Arial", 8, FontStyle.Regular);
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
                     if (dr.Cells["Col_ProductID"].Value != null)
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
                         if (dr.Cells["Col_ProdName"].Value != null)
                         {
                             row = new PrintRow(dr.Cells["Col_ProdName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                             PrintBill.Rows.Add(row);
                         }
                         if (dr.Cells["Col_UOM"].Value != null)
                         {
                             row = new PrintRow(dr.Cells["Col_UOM"].Value.ToString().PadRight(30), PrintRowPixel, 240, PrintFont);
                             PrintBill.Rows.Add(row);
                         }
                         if (dr.Cells["Col_Pack"].Value != null)
                         {
                             row = new PrintRow(dr.Cells["Col_Pack"].Value.ToString().PadRight(30), PrintRowPixel, 300, PrintFont);
                             PrintBill.Rows.Add(row);
                         }
                         if (dr.Cells["Col_Comp"].Value != null)
                         {
                             row = new PrintRow(dr.Cells["Col_Comp"].Value.ToString().PadRight(30), PrintRowPixel, 400, PrintFont);
                             PrintBill.Rows.Add(row);
                         }
                        
                         if (dr.Cells["Col_Quantity"].Value != null)
                         {
                             row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 480, PrintFont);
                             PrintBill.Rows.Add(row);
                         }                        
                         if (dr.Cells["Col_Amount"].Value != null)
                         {

                             mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                             mlen = (mamt.ToString("#0.00").Length);
                             colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                             row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                             PrintBill.Rows.Add(row);
                         }
                     }
                 }
                 PrintRowPixel += 17;

                 row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                 PrintBill.Rows.Add(row);
                 PrintRowPixel += 13;
                 mlen = (_MTotalAmount.ToString("#0.00").Length);
                 colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                 row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                 PrintBill.Rows.Add(row);
                 PrintRowPixel += 13;
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
                 PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);

                 PrintRowPixel += 17;

                 row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                 PrintBill.Rows.Add(row);

                 PrintRowPixel += 13;

                 row = new PrintRow("Product", PrintRowPixel, 1, PrintFont);
                 PrintBill.Rows.Add(row);
                 row = new PrintRow("UOM", PrintRowPixel, 240, PrintFont);
                 PrintBill.Rows.Add(row);
                 row = new PrintRow("Pack", PrintRowPixel, 300, PrintFont);
                 PrintBill.Rows.Add(row);
                 row = new PrintRow("Comp", PrintRowPixel, 380, PrintFont);
                 PrintBill.Rows.Add(row);                
                 row = new PrintRow("Qty", PrintRowPixel, 480, PrintFont);
                 PrintBill.Rows.Add(row);
                 row = new PrintRow("Amount", PrintRowPixel, 580, PrintFont);
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

     

        #endregion

        # region Other Private methods

         public void ClearControls()
         {
             try
             {                
                 lblFooterMessage.Text = "";
                 ConstructReportColumns();
                 InitializeReportGrid();
                 DataTable dtable = new DataTable();
                 dgvReportList.DataSource = dtable;
                 dgvReportList.Bind();
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
                column.Width = 250;
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
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_Rate";
                //column.DataPropertyName = "SaleRate";
                //column.HeaderText = "SaleRate";
                //column.Width = 100;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 150;
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

            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
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
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
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
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                dtable = _SaleList.GetSaleDataForProductSummary(_MFromDate, _MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }    
        #endregion

        #region Events

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            //string macccode = "";
            //try
            //{
            //    if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    
            //        if (dgvReportList.SelectedRow.Cells["Col_AccCode"].Value != null && dgvReportList.SelectedRow.Cells["Col_AccCode"].Value.ToString() != "")
            //            macccode = dgvReportList.SelectedRow.Cells["Col_AccCode"].Value.ToString();

            //         if (macccode != FixAccounts.AccCodeForDebtor)
            //            ViewControl = new UclPatientSale();
            //        else
            //            ViewControl = new UclDebtorSale();
            //         ShowViewForm(selectedID, ViewMode.Current);
            //        this.Cursor = Cursors.Default;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteError(ex.ToString());
            //}
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
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd"); 
                 retValue = General.CheckDates(_MFromDate, _MToDate);
                 if (retValue)
                 {
                     this.Cursor = Cursors.WaitCursor;
                     FillReportGrid();
                     ShowpnlGO();
                     this.Cursor = Cursors.Default;
                     dgvReportList.Focus();
                 }
                 else
                     lblFooterMessage.Text = "Please Check Date...";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                btnOKMultiSelectionClick();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
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
