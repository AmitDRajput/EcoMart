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
    public partial class UclVouChequeReturnList : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private ChequeReturn _ChequeReturn;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        # endregion

        #region Constructor
        public UclVouChequeReturnList()
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

        # region IOverview
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _ChequeReturn = new ChequeReturn();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "CHEQUE RETURN LIST";
                InitializeReportGrid();
                AddToolTip();
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
                //if (txtViewType.Text != null && txtViewType.Text.ToString() != "")
                //    PrintReportHead2 = PrintReportHead2 + "Type : " + txtViewType.Text.ToString() + "  ";
                //if (txtViewAmount.Text != null && txtViewAmount.Text.ToString() != "")
                //    PrintReportHead2 = PrintReportHead2 + "Amount : " + txtViewAmount.Text.ToString();

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
                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString(), PrintRowPixel, 50, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 110, PrintFont);
                        PrintBill.Rows.Add(row);
                        int length = Math.Min(dr.Cells["Col_AccName"].Value.ToString().Length, 20);
                        row = new PrintRow((dr.Cells["Col_AccName"].Value.ToString()).Substring(0, length), PrintRowPixel, 170, PrintFont);
                        PrintBill.Rows.Add(row);
                        length = Math.Min(dr.Cells["Col_Address"].Value.ToString().Length, 15);
                        row = new PrintRow(dr.Cells["Col_Address"].Value.ToString().Substring(0, length), PrintRowPixel, 380, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_CVoucherType"].Value.ToString(), PrintRowPixel, 540, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_CVoucherNumber"].Value != null && dr.Cells["Col_CVoucherNumber"].Value.ToString() != "0")
                            row = new PrintRow(dr.Cells["Col_CVoucherNumber"].Value.ToString(), PrintRowPixel, 580, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_CVoucherDate"].Value != null && dr.Cells["Col_CVoucherDate"].Value.ToString() != "")
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_CVoucherDate"].Value.ToString()), PrintRowPixel, 620, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_Amount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(680.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
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
                colpix = Convert.ToInt32(680.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 40, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 110, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 170, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 380, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Cleared In Type", PrintRowPixel, 480, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 580, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 640, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 700, PrintFont);
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
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
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
                column.DataPropertyName = "CBID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 150;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "AccountID";
                column.Width = 180;
                column.Visible = false;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 250;
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
                dgvReportList.DataSource = _BindingSource;
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
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DateColumnNames.Add("Col_CVoucherDate");
        }
        private void BindReportGrid()
        {
            try
            {
                _MTotalAmount = 0;
              
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["CBID"].ToString() != null)
                    {
                        string drID = "";
                        string drType = "";
                        double drAmt = 0;
                        if (dr["AccountId"] != DBNull.Value)
                            drID = dr["AccountID"].ToString();
                        if (dr["ChequeReturnVoucherType"] != DBNull.Value)
                            drType = dr["ChequeReturnVoucherType"].ToString();
                        if (dr["AmountNet"] != DBNull.Value)
                            drAmt = Convert.ToDouble(dr["AmountNet"].ToString());                      
                      
                            int rowIndex = dgvReportList.Rows.Add();
                            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                            dgvrow.Cells["Col_ID"].Value = dr["CBID"].ToString();
                            dgvrow.Cells["Col_VoucherType"].Value = dr["ChequeReturnVoucherType"].ToString();
                            dgvrow.Cells["Col_VoucherNumber"].Value = dr["ChequeReturnVoucherNumber"].ToString();
                            dgvrow.Cells["Col_VoucherDate"].Value = dr["ChequeReturnVoucherDate"].ToString();
                            dgvrow.Cells["Col_AccountID"].Value = dr["AccountID"].ToString();
                            dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                            dgvrow.Cells["Col_Address"].Value = dr["AccAddress1"].ToString();
                          //  dgvrow.Cells["Col_CVoucherType"].Value = dr["ClearedInVoucherType"].ToString();
                          //  dgvrow.Cells["Col_CVoucherNumber"].Value = dr["ClearedInVoucherNumber"].ToString();
                         //   if (dr["ClearedInVoucherDate"].ToString() != "")
                          //      dgvrow.Cells["Col_CVoucherDate"].Value = dr["ClearedInVoucherDate"].ToString();
                            dgvrow.Cells["Col_Amount"].Value = dr["AmountNet"].ToString();
                            _MTotalAmount += Convert.ToDouble(dr["AmountNet"].ToString());
                        
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
       

        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _ChequeReturn.GetOverviewData(_MFromDate, _MToDate);
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
        # endregion Other Private Methods

        # region events
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
                    //if (mcbType.Text != null && mcbType.Text != "")
                    //    txtViewType.Text = mcbType.Text.ToString();
                    //else
                    //    txtViewType.Text = "";
                    //if (Convert.ToDouble(txtAmount.Text.ToString()) != 0)
                    //    txtViewAmount.Text = txtAmount.Text.ToString();
                    //else
                    //    txtViewAmount.Text = "";
                    lblFooterMessage.Text = "";
                    ViewFromDate.Text = (_MFromDate);
                    ViewToDate.Text = (_MToDate);
                    FillReportGrid();
                    PrintReportHead = "DEBIT NOTE  LIST From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
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
            string mvouchertype = "";
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    if (dgvReportList.SelectedRow.Cells["Col_VoucherType"] != null)
                    {
                        mvouchertype = dgvReportList.SelectedRow.Cells["Col_VoucherType"].Value.ToString();

                        ViewControl = new UclChequeReturn();

                        ShowViewForm(selectedID, ViewMode.Current);
                    }
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

        private void btnOKMultiSelection_KeyDown(object sender, KeyEventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        # endregion Events

        #region AddToolTip
        private void AddToolTip()
        {
            try
            {
                //ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                //ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion AddToolTip

    }
}
