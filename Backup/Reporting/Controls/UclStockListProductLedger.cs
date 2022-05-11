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
    public partial class UclStockListProductLedger : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SsStock _SsStock;
        private string _MFromDate;
        private string _MToDate;
        private int _OpeningStock;
        #endregion

        # region Constructor
        public UclStockListProductLedger()
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
                FillProductCombo();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void ShowReport(string ID, string FromDate, string ToDate)
        {
            base.ShowReport(ID, FromDate, ToDate);
            if (ID != null && ID != "")
            {
                _MFromDate = FromDate;
                _MToDate = ToDate;
                mcbProduct.SelectedID = ID;
                _OpeningStock = _SsStock.GetOpendingStockByProductID(ID);
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
                PrintReportHead = "Product Ledger  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "[" + mcbProduct.SeletedItem.ItemData[1].ToString() + " " + mcbProduct.SeletedItem.ItemData[2].ToString() + " " + mcbProduct.SeletedItem.ItemData[3].ToString() + "]";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);

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
            PrintRowCount = 7;
            return PrintRowCount;
        }

        #endregion IReportMember

        # region Other Private methods

        public void ClearControls()
        {
            try
            {                
                fromDate1.Value = DateTime.Now;
                toDate1.Value = DateTime.Now;
                lblFooterMessage.Text = "";
                mcbProduct.SelectedID = null;
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
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Type";
                column.Width = 80;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.Visible = false;
                column.Width = 80;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 280;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 150;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_QtyIN";
                column.DataPropertyName = "QuantityIN";
                column.HeaderText = "Qty";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 70;
                
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmIN";
                column.DataPropertyName = "SchemeQuantityIN";
                column.HeaderText = "Scm";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_QtyOUT";
                column.DataPropertyName = "QuantityOUT";
                column.HeaderText = "Qty";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmOUT";
                column.DataPropertyName = "SchemeQuantityOUT";
                column.HeaderText = "Scm";
                column.Width = 70;
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
            mcbProduct.SelectedID = "";
            txtViewText.Text = "";
            toDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            if (_MFromDate != null)
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            if (_MToDate != null)
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            txtViewText.Text = mcbProduct.SeletedItem.ItemData[1].ToString() + " " + mcbProduct.SeletedItem.ItemData[2].ToString() + " " + mcbProduct.SeletedItem.ItemData[3].ToString();
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
               dtable = _SsStock.GetDataForProductLedger(mcbProduct.SelectedID, _MToDate);
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
            int _RowIndex;
            DataGridViewRow currentdr;          
            int mopstk = 0;
            int mclstk = 0;
            string mvoudate = "";
            int mqtyin = 0;
            int mqtyout = 0;
            int mscmqtyin = 0;
            int mscmqtyout = 0;
            int mcrstk = 0;
            int mdbstk = 0;
            int mpurstk = 0;
            int mslstk = 0;
            string mvoutype = "";
            string msubtype = "";
            foreach (DataRow dr in _BindingSource.Rows)
            {
                mvoudate = "";
                mqtyin = 0;
                mqtyout = 0;
                mscmqtyin = 0;
                mscmqtyout = 0;
                mvoutype = "";
                msubtype = "";
                if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    mvoudate = dr["VoucherDate"].ToString();
                if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() != "")
                    mvoutype = dr["VoucherType"].ToString();
                if (dr["VoucherSubType"] != DBNull.Value && dr["VoucherSubType"].ToString() != "")
                    msubtype = dr["VoucherSubType"].ToString();
                if (dr["QuantityIN"] != DBNull.Value && dr["QuantityIN"].ToString() != "")
                    int.TryParse(dr["QuantityIN"].ToString(), out mqtyin);
                if (dr["QuantityOUT"] != DBNull.Value && dr["QuantityOUT"].ToString() != "")
                    int.TryParse(dr["QuantityOUT"].ToString(), out mqtyout);
                if (dr["SchemeQuantityIN"] != DBNull.Value && dr["SchemeQuantityIN"].ToString() != "")
                    int.TryParse(dr["SchemeQuantityIN"].ToString(), out mscmqtyin);
                if (dr["SchemeQuantityOUT"] != DBNull.Value && dr["SchemeQuantityOUT"].ToString() != "")
                    int.TryParse(dr["SchemeQuantityOUT"].ToString(), out mscmqtyout);
                if (Convert.ToInt32(mvoudate) < Convert.ToInt32(_MFromDate))

                    mopstk = mopstk + mqtyin - mqtyout + mscmqtyin - mscmqtyout;
                else
                {
                    _RowIndex = dgvReportList.Rows.Add();
                    currentdr = dgvReportList.Rows[_RowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = mvoutype;
                    currentdr.Cells["Col_VoucherSubType"].Value = msubtype;
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(mvoudate);
                    currentdr.Cells["Col_AccountID"].Value = dr["AccountID"].ToString();
                    currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                    currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                    currentdr.Cells["Col_QtyIN"].Value = dr["QuantityIN"].ToString();
                    currentdr.Cells["Col_ScmIN"].Value = dr["SchemeQuantityIN"].ToString();
                    currentdr.Cells["Col_QtyOUT"].Value = dr["QuantityOUT"].ToString();
                    currentdr.Cells["Col_ScmOUT"].Value = dr["SchemeQuantityOUT"].ToString();
                    if (mvoutype == FixAccounts.VoucherTypeForCreditPurchase || mvoutype == FixAccounts.VoucherTypeForCreditStatementPurchase || mvoutype == FixAccounts.VoucherTypeForCashPurchase)
                    {
                        mpurstk += (mqtyin + mscmqtyin);

                    }
                    if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock)
                    {
                        mcrstk += (mqtyin + mscmqtyin);
                    }
                    if (mvoutype == FixAccounts.VoucherTypeForVoucherSale || mvoutype == FixAccounts.VoucherTypeForCreditStatementSale || mvoutype == FixAccounts.VoucherTypeForCashSale || mvoutype == FixAccounts.VoucherTypeForCreditSale)
                    {
                        mslstk += (mqtyout + mscmqtyout);
                    }
                    if (mvoutype == FixAccounts.VoucherTypeForDebitNoteStock || mvoutype == FixAccounts.VoucherTypeForStockOut)
                    {
                        mdbstk += (mqtyout + mscmqtyout);
                    }
                }
            }
            

            _RowIndex = dgvReportList.Rows.Add();
            currentdr = dgvReportList.Rows[_RowIndex];
            currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
            currentdr.Cells["Col_AccName"].Value = "Opening Stock";
            currentdr.Cells["Col_QtyIN"].Value = (_OpeningStock + mopstk).ToString();
            _RowIndex = dgvReportList.Rows.Add();
            currentdr = dgvReportList.Rows[_RowIndex]; 
            currentdr.Cells["Col_AccName"].Value = "Purchase Stock";
            currentdr.Cells["Col_QtyIN"].Value = (mpurstk).ToString();
            _RowIndex = dgvReportList.Rows.Add();
            currentdr = dgvReportList.Rows[_RowIndex]; 
            currentdr.Cells["Col_AccName"].Value = "Credit Note Stock";
            currentdr.Cells["Col_QtyIN"].Value = (mcrstk).ToString();
            _RowIndex = dgvReportList.Rows.Add();
            currentdr = dgvReportList.Rows[_RowIndex]; 
            currentdr.Cells["Col_AccName"].Value = "Sale Stock";
            currentdr.Cells["Col_Qtyout"].Value = (mslstk).ToString();
            _RowIndex = dgvReportList.Rows.Add();
            currentdr = dgvReportList.Rows[_RowIndex]; 
            currentdr.Cells["Col_AccName"].Value = "Debit Note Stock";
            currentdr.Cells["Col_Qtyout"].Value = (mdbstk).ToString();
            _RowIndex = dgvReportList.Rows.Add();
            currentdr = dgvReportList.Rows[_RowIndex]; 
            mclstk = _OpeningStock + mopstk + mpurstk + mcrstk - mslstk - mdbstk;
            currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
            currentdr.Cells["Col_AccName"].Value = "Closing Stock";
            currentdr.Cells["Col_QtyIN"].Value = (mclstk).ToString();
           

        }
        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[6] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName","ProdOpeningStock"};
                mcbProduct.ColumnWidth = new string[6] { "0", "250", "50", "50", "50" ,"0"};
                mcbProduct.ValueColumnNo = 0;
                mcbProduct.UserControlToShow = new UclProduct();
                DataTable dtable = General.ProductList;
                mcbProduct.FillData(dtable);
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
                 _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != string.Empty)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        _OpeningStock = 0;
                        if (mcbProduct.SeletedItem.ItemData[5] != null && mcbProduct.SeletedItem.ItemData[5].ToString() != string.Empty)
                            _OpeningStock = Convert.ToInt32(mcbProduct.SeletedItem.ItemData[5].ToString());
                        FillReportGrid();
                        ShowpnlGO();
                        this.Cursor = Cursors.Default;
                        dgvReportList.Focus();
                    }
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
        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                btnOKMultiSelectionClick();
        }
      
        private void fromdate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void todate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbProduct.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
        }
        #endregion Events

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
                    ViewControl = null;
                    voutype = dgvReportList.SelectedRow.Cells[1].Value.ToString();
                    vousubtype = dgvReportList.SelectedRow.Cells["Col_VoucherSubType"].Value.ToString();
                    if (voutype == FixAccounts.VoucherTypeForCashPurchase || voutype == FixAccounts.VoucherTypeForCreditPurchase || voutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
                        ViewControl = new UclPurchase();
                    else if (voutype == FixAccounts.VoucherTypeForCreditNoteStock)
                        ViewControl = new UclCreditNoteStock();
                    else if (voutype == FixAccounts.VoucherTypeForDebitNoteStock)
                        ViewControl = new UclDebitNotestock();
                    else if (voutype == FixAccounts.VoucherTypeForOpeningStock)
                        ViewControl = new UclOPStock();
                    else if (voutype == FixAccounts.VoucherTypeForStockIN)
                        ViewControl = new UclStockIn();
                    else if (voutype == FixAccounts.VoucherTypeForStockOut)
                        ViewControl = new UclStockOut();
                    else if (vousubtype == FixAccounts.SubTypeForPatientSale)
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

       

    }
}
