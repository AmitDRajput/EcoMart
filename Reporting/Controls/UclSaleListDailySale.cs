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
using PharmaSYSPlus.CommonLibrary;
using EcoMart.Printing;
using PrintDataGrid;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSaleListDailySale : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MVoucherType;
        private VoucherTypes  myvouchertype;
        #endregion

        # region Constructor
        public UclSaleListDailySale()
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
                dgvReportList.DataSource = _BindingSource;               
                dgvReportList.Bind();
                headerLabel1.Text = "DAILY SALE";
                ClearControls();
                HidepnlGO();
                rbtnAll.Checked = true;
                AddToolTip();                 
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        
       

        public override bool Exit()
        {
            bool retValue = base.Exit();          
            return retValue;
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

        #endregion IOverviewMembers

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
                PrintHead(_MFromDate);
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

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
                            PrintHead(_MFromDate);
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 45, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 100, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 160, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_Address"].Value.ToString().PadRight(30), PrintRowPixel, 380, PrintFont);
                        PrintBill.Rows.Add(row);
                        double mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen))*5.5));                      
                        row = new PrintRow(mamt.ToString("#0.00") , PrintRowPixel, colpix , PrintFont);
                        PrintBill.Rows.Add(row);                       
                    }
                }
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;
                mlen = (PrintTotalAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(PrintTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintFooter(PrintRowPixel); // [03.02.2017]
                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHead(string _MFromDate) // [Modified on 06.02.2017]
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(_MFromDate), General.GetDateInDateFormat(_MFromDate));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 40, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 100, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 160, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 380, PrintFont);
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

        private int PrintFooter(int PrintRowPixel) // [03.02.2017]
        {
            try
            {
                PrintRowPixel = GeneralReport.PrintFooter(PrintTotalPages, PrintFont, PrintRowPixel, PrintPageNumber);

                //PrintRowPixel += 17;
                //row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                //PrintBill.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }
        #endregion IReport Member

        #region Other Private methods

        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                FormatReportGrid();
                InitializeDates();
                lblFooterMessage.Text = "";
                txtViewtype.Text = "";
                ViewFromDate.Text = "";
                if (General.CurrentSetting.MsetReportSaleDailySaleDoNotShowTotal == "Y")
                    txtReportTotalAmount.Visible = false;
                txtReportTotalAmount.Text = "";
                _BindingSource = null;
                _MVoucherType = "";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void InitializeDates()
        {
          //  _MFromDate = DateTime.Now.Date.ToString("yyyyMMdd");
            _MFromDate = General.TodayString;
            fromDate1.Value = General.TodayDateTime;

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
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Sub Type";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Patient";
                column.Width = 300;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                dgvReportList.Bind();
                PrintTotalAmount = 0;

                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    PrintTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                }

                txtReportTotalAmount.Text =  PrintTotalAmount.ToString("#0.00");
               
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
                InitializeReportGrid(); 
                _BindingSource = _SaleList.GetOverviewDataForDailySaleReport(_MFromDate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void CheckFilter()
        {
            try
            {
                if (rbtnCash.Checked == true)
                {


                   // txtType.Text = FixAccounts.VoucherTypeForCashPurchase;
                    txtViewtype.Text = "Cash";
                    _BindingSource.DefaultView.RowFilter = "VoucherType = '" + FixAccounts.VoucherTypeForCashSale+ "'";
                }
                else if (rbtnCreditStatement.Checked == true)
                {
                  //  txtType.Text = FixAccounts.VoucherTypeForCreditStatementPurchase;
                    txtViewtype.Text = "CreditStatement";
                    _BindingSource.DefaultView.RowFilter = "VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "'";
                }
                else if (rbtnCredit.Checked == true)
                {
                  //  txtType.Text = FixAccounts.VoucherTypeForCreditPurchase;
                    txtViewtype.Text = "Credit";
                    _BindingSource.DefaultView.RowFilter = "VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "'";
                }
                else if (rbtnVoucher.Checked == true)
                {
                    txtViewtype.Text = "Voucher";
                    _BindingSource.DefaultView.RowFilter = "VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "'";
                }
                else if (rbtnCreditCard.Checked == true)
                {
                    txtViewtype.Text = "CreditCard";
                 //   _BindingSource.DefaultView.RowFilter = "VoucherSubType = '" + FixAccounts.SubTypeForCreditCardSale + "'";
                }
                else
                {
                    //   txtType.Text = "";
                    txtViewtype.Text = "ALL";
                    _BindingSource.DefaultView.RowFilter = "";
                }
            }
            catch (Exception ex) { Log.WriteException(ex); }
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
                retValue = General.CheckDates(_MFromDate, _MFromDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();
                    ShowpnlGO();
                    FillReportGrid();
                    PrintReportHead = "Daily Sale"; // + General.GetDateInDateFormat(_MFromDate); [modified on 06.02.2017]
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
                    if (vousubtype == FixAccounts.SubTypeForRegularSale )
                        ShowViewForm(selectedID, ViewMode.Current);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void dateVoucherTypeControlOneDateSale1_OnGoClicked(DateTime FromDate, VoucherTypes vType)
        {           
            OnGoClick(FromDate,vType);
        }

        private void OnGoClick(DateTime FromDate, VoucherTypes vType)
        {
            bool retValue = false;
            try
            {               
                _MFromDate = FromDate.Date.ToString("yyyyMMdd");
                retValue =  General.CheckDates(_MFromDate, _MFromDate);
                myvouchertype = vType;
                if (retValue)
                {
                    lblFooterMessage.Text = "";
                    string mvoutype = vType.ToString();
                    if (mvoutype == "All")
                        _MVoucherType = "All";
                    else if (mvoutype == "Cash")
                        _MVoucherType = FixAccounts.VoucherTypeForCashSale;
                    else if (mvoutype == "Credit")
                        _MVoucherType = FixAccounts.VoucherTypeForCreditSale;
                    else if (mvoutype == "CreditStatement")
                        _MVoucherType = FixAccounts.VoucherTypeForCreditStatementSale;
                    else
                        _MVoucherType = FixAccounts.VoucherTypeForVoucherSale;

                 
                    ShowpnlGO();
                    FillReportGrid();
                }
                else
                    lblFooterMessage.Text = "Please Check Date...";

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {                
               // ttToolTip.SetToolTip(dateVoucherTypeControlOneDateSale1, "Home  To Reopen This Form   End To See Report");               
                ttToolTip.SetToolTip(dgvReportList, "Home key To Select Options");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
       

      
        #endregion

      
    }
}
