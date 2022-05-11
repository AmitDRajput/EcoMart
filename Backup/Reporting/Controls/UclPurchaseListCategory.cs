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
    public partial class UclPurchaseListCategory : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        public DataTable selectedcompanies;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        private double _MTotalAmountCash;
        private double _MTotalAmountSPL;
        private double _MTotalAmountSCM;
        private double _MTotalAmountItem;
        #endregion

        # region Constructor
        public UclPurchaseListCategory()
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
                _Purchase = new Purchase();               
                headerLabel1.Text = "PURCHASE-CATEGORY";
                ClearControls();
                HidepnlGO();
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
        #endregion IOverViewMembers

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
                PrintReportHead = "Purchase Category From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
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
                    if (dr.Cells["Col_Name"].Value != null)
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
                        if (dr.Cells["Col_Name"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Name"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Amount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(220.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                       
                      
                        if (dr.Cells["Col_Item"].Value != null && Convert.ToDouble(dr.Cells["Col_Item"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Item"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(340.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Spl"].Value != null && Convert.ToDouble(dr.Cells["Col_Spl"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Spl"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(440.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_SCM"].Value != null && Convert.ToDouble(dr.Cells["Col_SCM"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_SCM"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(540.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Cash"].Value != null && Convert.ToDouble(dr.Cells["Col_Cash"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Cash"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(640.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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
                colpix = Convert.ToInt32(220.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);

                mlen = (_MTotalAmountItem.ToString("#0.00").Length);
                colpix = Convert.ToInt32(340.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmountItem.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);

                mlen = (_MTotalAmountSPL.ToString("#0.00").Length);
                colpix = Convert.ToInt32(440.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmountSPL.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);

                mlen = (_MTotalAmountSCM.ToString("#0.00").Length);
                colpix = Convert.ToInt32(540.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmountSCM.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);

                mlen = (_MTotalAmountCash.ToString("#0.00").Length);
                colpix = Convert.ToInt32(640.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmountCash.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
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
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Category", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 250, PrintFont);
                PrintBill.Rows.Add(row);               
                row = new PrintRow("Item Disc", PrintRowPixel, 370, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Spl Disc", PrintRowPixel, 470, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Scm Disc", PrintRowPixel, 570, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Cash Disc", PrintRowPixel, 670, PrintFont);
                PrintBill.Rows.Add(row);               
                PrintRowPixel += 13;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 7;
            return PrintRowCount;
        }
        #endregion IReport Memebers

        # region Other Private methods

        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                InitializeDates();          
                lblFooterMessage.Text = "";
                txtReportTotalAmount.Text = "";
                txtTotCashDiscount.Text = "";
                txtTotItemDiscount.Text = "";
                txtTotScmDiscount.Text = "";
                txtTotSplDiscount.Text = "";
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
                column.Name = "Col_Name";
                column.DataPropertyName = "ProductCategoryName";
                column.HeaderText = "Category";
                column.Width = 280;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount (Quantity * TradeRate)";
                column.Width = 260;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Item";
                column.DataPropertyName = "AmountItemDiscount";
                column.HeaderText = "Item Disc";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Spl";
                column.DataPropertyName = "AmountSpecialDiscount";
                column.HeaderText = "Special Disc";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SCM";
                column.DataPropertyName = "AmountSchemeDiscount";
                column.HeaderText = "Scheme Disc";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Cash";
                column.DataPropertyName = "AmountCashDiscount";
                column.HeaderText = "Cash Disc";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
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
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
                _MTotalAmount = 0;
                _MTotalAmountCash = 0;
                _MTotalAmountItem = 0;
                _MTotalAmountSCM = 0;
                _MTotalAmountSPL = 0;

                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                    _MTotalAmountItem += Convert.ToDouble(dr.Cells["Col_Item"].Value.ToString());
                    _MTotalAmountSPL += Convert.ToDouble(dr.Cells["Col_Spl"].Value.ToString());
                    _MTotalAmountSCM += Convert.ToDouble(dr.Cells["Col_SCM"].Value.ToString());
                    _MTotalAmountCash += Convert.ToDouble(dr.Cells["Col_Cash"].Value.ToString());

                }

                txtReportTotalAmount.Text = _MTotalAmount.ToString("#0.00");
                txtTotItemDiscount.Text = _MTotalAmountItem.ToString("#0.00");
                txtTotSplDiscount.Text = _MTotalAmountSPL.ToString("#0.00");
                txtTotScmDiscount.Text = _MTotalAmountSCM.ToString("#0.00");
                txtTotCashDiscount.Text = _MTotalAmountCash.ToString("#0.00");

                NoofRows();
                dgvReportList.Focus();
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
                dtable = _Purchase.GetOverviewDataCategory(_MFromDate,_MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void InitializeDates()
        {
            fromDate1.Value = DateTime.Now;
            toDate1.Value = DateTime.Now;
        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            txtReportTotalAmount.Text = "";
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
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
                    InitializeReportGrid();                                               
                    FillReportGrid();
                    ShowpnlGO();
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
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    ShowViewForm(selectedID, ViewMode.Current);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
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

        #region ToolTip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion ToolTip

       
    }
}
