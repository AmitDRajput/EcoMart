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
    public partial class UclListNextVisitDate : ReportBaseControl
    {

        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;       
        #endregion

        public UclListNextVisitDate()
        {
            InitializeComponent();
        }

        #region IOverview Members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _SaleList = new SaleList();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "VAT - SALES REGISTER TINWISE SUMMARY";
                ConstructReportColumns();
                dgvReportList.Columns["Col_ID"].Visible = false;
                ClearControls();
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
                //int mlen = 0;
               // int colpix = 0;
                //double mamt = 0;
                // totalrows += 7; // for first page heading
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_AccName"].Value != null)
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
                        //mamt = 0;

                        //if (dr.Cells["Col_AccName"].Value.ToString() != null)
                        //{
                        //    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                        //    PrintBill.Rows.Add(row);
                        //    PrintRowPixel += 17;
                        //    PrintRowCount += 1;
                        //}

                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_NextVisitDate"].Value.ToString()), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(dr.Cells["Col_MobileNumberForSMS"].Value.ToString(), PrintRowPixel, 150, PrintFont);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(25).Substring(0, 25), PrintRowPixel, 340, PrintFont);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(dr.Cells["Col_Address"].Value.ToString().PadRight(25), PrintRowPixel, 530, PrintFont);
                        PrintBill.Rows.Add(row);

                        //if (dr.Cells["Col_TotalAmount"].Value != null)
                        //{

                        //    mamt = Convert.ToDouble(dr.Cells["Col_TotalAmount"].Value.ToString());
                        //    mlen = (mamt.ToString("#0.00").Length);
                        //    colpix = Convert.ToInt32(235.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        //    PrintBill.Rows.Add(row);
                        //}

                        //if (dr.Cells["Col_TotalVAT"].Value != null)
                        //{

                        //    mamt = Convert.ToDouble(dr.Cells["Col_TotalVAT"].Value.ToString());
                        //    mlen = (mamt.ToString("#0.00").Length);
                        //    colpix = Convert.ToInt32(335.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        //    PrintBill.Rows.Add(row);
                        //}
                        //if (dr.Cells["Col_5perVAT"].Value != null)
                        //{

                        //    mamt = Convert.ToDouble(dr.Cells["Col_5perVAT"].Value.ToString());
                        //    mlen = (mamt.ToString("#0.00").Length);
                        //    colpix = Convert.ToInt32(435.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        //    PrintBill.Rows.Add(row);
                        //}
                        //if (dr.Cells["Col_12point5perVAT"].Value != null)
                        //{

                        //    mamt = Convert.ToDouble(dr.Cells["Col_12point5perVAT"].Value.ToString());
                        //    mlen = (mamt.ToString("#0.00").Length);
                        //    colpix = Convert.ToInt32(535.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        //    PrintBill.Rows.Add(row);
                        //}
                        //if (dr.Cells["Col_TIN"].Value != null)
                        //    row = new PrintRow(dr.Cells["Col_TIN"].Value.ToString(), PrintRowPixel, 635, PrintFont);
                        //PrintBill.Rows.Add(row);

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
                row = new PrintRow("NextVisitDate", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Mobile No", PrintRowPixel, 150, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 340, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 530, PrintFont);
                PrintBill.Rows.Add(row);
                //row = new PrintRow("13.5% VAT", PrintRowPixel, 565, PrintFont);
                //PrintBill.Rows.Add(row);
                //row = new PrintRow("TIN", PrintRowPixel, 665, PrintFont);
                //PrintBill.Rows.Add(row);
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

        #region OtherPrivateMethods

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
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            dgvReportList.InitializeColumnContextMenu();
        }
        private void InitializeDates()
        {
            fromDate1.Value = DateTime.Now;
            toDate1.Value = DateTime.Now;
            //fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            //toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
        }
        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            dgvStockList.Visible = false;
            ViewFromDate.Visible = false;
            ViewToDate.Visible = false;
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            //if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            //{
            //  //  tsbtnPrint.Enabled = true;
            //}
            ViewFromDate.Visible = true;
            ViewToDate.Visible = true;
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
                column.DataPropertyName = "ID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 60;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_NextVisitDate";
                column.DataPropertyName = "NextVisitDate";
                column.HeaderText = "Date";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MobileNumberForSMS";
                column.DataPropertyName = "MobileNumberForSMS";
                column.HeaderText = "Mobile Number";
                column.Width = 150;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "AccountID";
                column.Visible = false;
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
                column.Width = 200;
                dgvReportList.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructStockListColumns()
        {

            dgvStockList.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "ProductID";
            column.HeaderText = "ID";
            column.Visible = false;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProductName";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "Product";
            column.Width = 200;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_UOM";
            column.DataPropertyName = "ProdLoosePack";
            column.HeaderText = "UOM";
            column.Width = 70;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Pack";
            column.DataPropertyName = "ProdPack";
            column.HeaderText = "Pack";
            column.Width = 70;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_CompanyShortName";
            column.DataPropertyName = "ProdCompShortName";
            column.HeaderText = "Comp";
            column.Width = 40;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_MRP";
            column.DataPropertyName = "ProdLastPurchaseMRP";
            column.HeaderText = "MRP";
            column.Width = 90;
            column.Visible = false;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ClosingStock";
            column.DataPropertyName = "ProdClosingStock";
            column.HeaderText = "CL Stock";
            column.Width = 80;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_RequStock";
            column.DataPropertyName = "RequiredQuantity";
            column.HeaderText = "Required Stk";
            column.Width = 80;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Difference";
            column.DataPropertyName = "Difference";
            column.HeaderText = "Difference";
            column.Width = 80;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccName";
            column.DataPropertyName = "AccName";
            column.HeaderText = "Party";
            column.Width = 150;
            dgvStockList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.DataPropertyName = "AccName";
            column.HeaderText = "Party";
            column.Width = 150;
            column.Visible = false;
            dgvStockList.Columns.Add(column);
        }

        private void FillReportGrid()
        {
            try
            {
                FillReportData();
                dgvReportList.DataSource = _BindingSource;
                FormatReportGrid();
                dgvReportList.Bind();
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
                _BindingSource = new DataTable();
                _BindingSource =  _SaleList.GetNextVisitDays(_MFromDate, _MToDate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_NextVisitDate");
        }

        private void BindReportGrid()
        {
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    int rowIndex = dgvReportList.Rows.Add();
                    DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                    dgvrow.Cells["Col_ID"].Value = dr["ID"].ToString();
                    if (dr["AccVATTinNumber"] != DBNull.Value)
                        dgvrow.Cells["Col_TIN"].Value = dr["AccVATTinNumber"].ToString();
                    dgvrow.Cells["Col_TotalAmount"].Value = dr["TotalAmount"].ToString();
                    dgvrow.Cells["Col_TotalVAT"].Value = dr["TotalVAT"].ToString();
                    dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                    dgvrow.Cells["Col_5perVAT"].Value = dr["VAT5Per"].ToString();
                    dgvrow.Cells["Col_12point5perVAT"].Value = dr["VAT12point5Per"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();
                    ShowpnlGO();
                    PrintReportHead = "Next Visit Date From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
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

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }
        

        #endregion

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
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
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion

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

        private void btnCheckStock_Click(object sender, EventArgs e)
        {
            if (dgvStockList.Visible == false)
            {
                ConstructStockListColumns();
                dgvStockList.Columns[0].Visible = false;
                DataTable _BindingSourceStock = new DataTable();
                _BindingSourceStock = _SaleList.GetStockForNextVisitDays(_MFromDate, _MToDate);
                dgvStockList.DataSource = _BindingSourceStock;
                dgvStockList.Bind();
                dgvStockList.Visible = true;
            }
            else
                dgvStockList.Visible = false;
        }
    }
}
