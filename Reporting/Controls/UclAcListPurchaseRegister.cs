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
using System.IO;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclAcListPurchaseRegister : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;    
        #endregion

        # region Constructor
        public UclAcListPurchaseRegister()
        {
            try
            {
                InitializeComponent();
                //ViewControl = new UclPurchase();
             //////????   dateVoucherTypeControl1.OnGoClicked += new EcoMart.Reporting.Base.DateVoucherTypeControl.GoClicked(dateVoucherTypeControl1_OnGoClicked);
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
                _Purchase = new Purchase(); 
                headerLabel1.Text = "ACCOUNT - PURCHASE REGISTER";              
                ClearControls();
                AddToolTip();
                rbtnAll.Checked = true; 
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
                BtnOKMultiselectionClick();
                retValue = true;
            }
            //if (keyPressed == Keys.F11)
            //{
            //    fromDate1.Focus();
            //    retValue = true;
            //}
            //if (keyPressed == Keys.G && modifier == Keys.Alt)
            //{
            //    if (pnlMultiSelection1.Visible == true)
            //    {
            //        btnOKMultiSelectionClick();
            //        retValue = true;
            //    }
            //}
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
                double mamt = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_VoucherType"].Value != null || dr.Cells["Col_AccName"].Value != null)
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
                        mamt = 0;
                        if (dr.Cells["Col_AccName"].Value.ToString() == "Total")
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                        }
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
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 80, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30).Substring(0, 15), PrintRowPixel, 140, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Address"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Address"].Value.ToString().PadRight(30).Substring(0, 15), PrintRowPixel, 340, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Amount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(520.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                    }



                }
                PrintRowPixel += 17;
                PrintRowCount += 1;
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 50, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 80, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 340, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 540, PrintFont);
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
            _MFromDate = General.ShopDetail.Shopsy;
            _MToDate = General.ShopDetail.Shopey;           
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
                column.Name = "Col_ACCID";
                column.DataPropertyName = "AccountID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "TYPE";
                column.Width = 50;
                column.Visible = false;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";
                column.Width = 100;
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

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_VAT";
                //column.DataPropertyName = "AmountVAT";
                //column.HeaderText = "VAT";
                //column.Width = 110;
                //column.DefaultCellStyle.Format = "N2";
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 110;
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

        private void FillReportGrid()
        {
            try
            {
              
                FillReportData();
                dgvReportList.DataSource = _BindingSource;
                CheckFilter();
                FormatReportGrid();
                BindReportGrid();
                CalculateFinalTotals();
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
                if (txtType.Text != "")
                    dtable = _Purchase.GetOverviewDataForPurchaseRegister(_MFromDate, _MToDate, txtType.Text);
                else
                    dtable = _Purchase.GetOverviewDataForPurchaseRegister(_MFromDate, _MToDate);
              //  dtable = _Purchase.GetOverviewData();
                _BindingSource = dtable;
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
        }

        private void BindReportGrid()
        {
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (txtType.Text.ToString() == "" || (dr["VoucherType"].ToString() == txtType.Text.ToString()))
                    {
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                        dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        dgvrow.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                        dgvrow.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        dgvrow.Cells["Col_Address"].Value = dr["AccAddress1"].ToString();
                        dgvrow.Cells["Col_Amount"].Value = dr["AmountNet"].ToString();
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

   

        private void CheckFilter()
        {
            try
            {
                if (rbtnCash.Checked == true)
                {
                    txtType.Text = FixAccounts.VoucherTypeForCashPurchase;
                    txtViewtext.Text = "Cash";
                    _BindingSource.DefaultView.RowFilter = "VoucherType = '" + FixAccounts.VoucherTypeForCashPurchase + "'";
                }
                else if (rbtnCreditStatement.Checked == true)
                {
                    txtType.Text = FixAccounts.VoucherTypeForCreditStatementPurchase;
                    txtViewtext.Text = "CreditStatement";
                }
                else if (rbtnCredit.Checked == true)
                {
                    txtType.Text = FixAccounts.VoucherTypeForCreditPurchase;
                    txtViewtext.Text = "Credit";
                }
                else 
                {
                    txtType.Text = "";
                    txtViewtext.Text = "ALL";
                    _BindingSource.DefaultView.RowFilter = "";
                }
            }
            catch (Exception ex) { Log.WriteException(ex); }
        }

        private void CalculateFinalTotals()
        {
            _MTotalAmount = 0;
            //_MTotalVAT5 = 0;
            //_MTotalVAT12point5 = 0;
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                if (dr.Cells["Col_Amount"].Value != null)
                {
                    _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                    //_MTotalVAT5 += Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                    //_MTotalVAT12point5 += Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                }
            }
            int rowIndex = dgvReportList.Rows.Add();
            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
            dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
            dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
            dgvrow.Cells["Col_AccName"].Value = "Total";
            //dgvrow.Cells["Col_VAT5Percent"].Value = _MTotalVAT5.ToString("#0.00");
            //dgvrow.Cells["Col_VAT12Point5Percent"].Value = _MTotalVAT12point5.ToString("#0.00");
            dgvrow.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");
        }

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }       

      
        #endregion

        # region Events
        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            BtnOKMultiselectionClick();
        }

        private void BtnOKMultiselectionClick()
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
                    PrintReportHead = "Purchase Register [" + txtViewtext.Text.ToString() + "] From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "";
                    FillReportGrid();
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
                    string vousubtype = dgvReportList.SelectedRow.Cells["Col_VoucherSubType"].Value.ToString();
                    if (vousubtype == "1")
                        ViewControl = new UclPurchase();
                    //else
                    //    ViewControl = new UclPurchaseWithoutStock();
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
                BtnOKMultiselectionClick();
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                //ttToolTip.SetToolTip(btnOKMultiSelection1, "F10 = Show Report");
                //ttToolTip.SetToolTip(pnlMultiSelection1, "F12 = Reopen This Form , F11 = Date ");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion       

       
    }
}
