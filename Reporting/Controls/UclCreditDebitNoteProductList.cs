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
    public partial class UclCreditDebitNoteProductList : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;       
        private CreditDebitNote _CreditDebitNote;
        private string _MFromDate;
        private string _MToDate;
        private int _MTotalCN;
        private int _MTotalScmCN;
        private int _MTotalDN;
        private int _MTotalScmDN;
        # endregion

        #region Constructor
        public UclCreditDebitNoteProductList()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclDebitNotestock();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region IOverview

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _CreditDebitNote = new CreditDebitNote();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "DEBIT NOTE LIST (PRODUCT)";
                InitializeReportGrid();
                FillProductCombo();
                ClearControls();
                AddToolTip();
                HidepnlGO();
                mcbProduct.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void SetFocus()
        {
            mcbProduct.Focus();
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

        #endregion IOverview member

        #region IReport Control

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
                    PrintReportHead2 = "Product : " + "[" + txtViewtext.Text.ToString() + "]  ";

                PrintBill.Rows.Clear();                
                PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 0;
                PrintRowPixel = 0;                
                int minteger = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_ID"].Value != null)
                    {
                        if (PrintRowCount > FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel = 325;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintHead();
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;

                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        minteger = Convert.ToInt32(dr.Cells["Col_VoucherNumber"].Value.ToString());
                        row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 40, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 100, PrintFont);
                        PrintBill.Rows.Add(row);
                        minteger = 0;
                        if (dr.Cells["Col_QuantityCN"].Value != null)
                            int.TryParse(dr.Cells["Col_QuantityCN"].Value.ToString(), out minteger);
                        if (minteger > 0)
                        {
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 200, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        minteger = 0;
                        if (dr.Cells["Col_ScmQuantityCN"].Value != null)
                           int.TryParse(dr.Cells["Col_ScmQuantityCN"].Value.ToString(), out minteger);
                        if (minteger > 0)
                        {
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 260, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        minteger = 0;
                        if (dr.Cells["Col_QuantityDN"].Value != null)
                            int.TryParse(dr.Cells["Col_QuantityDN"].Value.ToString(), out minteger);
                        if (minteger > 0)
                        {
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 320, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        minteger = 0;
                        if (dr.Cells["Col_ScmQuantityDN"].Value != null)
                            int.TryParse(dr.Cells["Col_ScmQuantityDN"].Value.ToString(), out minteger);
                        if (minteger > 0)
                        {
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 380, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                       
                        int lengh = Math.Min(dr.Cells["Col_AccName"].Value.ToString().Length, 18);
                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(20).Substring(0, lengh), PrintRowPixel, 420, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_Address"].Value != null)
                        {
                            lengh = Math.Min(dr.Cells["Col_Address"].Value.ToString().Length, 10);
                            row = new PrintRow(dr.Cells["Col_Address"].Value.ToString().PadRight(10).Substring(0, lengh), PrintRowPixel, 620, PrintFont);
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
                row = new PrintRow("Number", PrintRowPixel, 40, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 100, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty CN", PrintRowPixel, 175, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM CN", PrintRowPixel, 235, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty DN", PrintRowPixel, 295, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM DN", PrintRowPixel, 355, PrintFont);
                PrintBill.Rows.Add(row);               
                row = new PrintRow("Party", PrintRowPixel, 420, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 620, PrintFont);
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

        #endregion IReport control

        #region Other Private Methods

        public void ClearControls()
        {
            try
            {
                InitializeDates();
                mcbProduct.SelectedID = "";
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
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
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
                column.DataPropertyName = "CRDBID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_ProductName";
                //column.DataPropertyName = "ProdName";
                //column.HeaderText = "Product";
                //column.Width = 180;
                //dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_UOM";
                //column.DataPropertyName = "ProdLoosePack";
                //column.HeaderText = "UOM";
                //column.Width = 50;
                //dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_ProductPack";
                //column.DataPropertyName = "ProdPack";
                //column.HeaderText = "Pack";
                //column.Width = 60;
                //dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_CompanyShortName";
                //column.DataPropertyName = "ProdCompShortName";
                //column.HeaderText = "Comp";
                //column.Width = 50;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_QuantityCN";              
                column.HeaderText = "Qty CN";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmQuantityCN";            
                column.HeaderText = "SCM CN";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_QuantityDN";
                column.HeaderText = "Qty DN";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmQuantityDN";
                column.HeaderText = "SCM DN";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";               
                column.HeaderText = "AccountID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";              
                column.HeaderText = "Party";
                column.Width = 180;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";               
                column.HeaderText = "Address";
                column.Width = 170;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdID";              
                column.HeaderText = "ID";
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
        }

        private void BindReportGrid()
        {
            _MTotalCN = 0;
            _MTotalDN = 0;
            _MTotalScmCN = 0;
            _MTotalScmDN = 0;
            try
            {

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["CRDBID"].ToString() != null)
                    {
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                       dgvrow.Cells["Col_ID"].Value = dr["CRDBID"].ToString();
                       dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                       dgvrow.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                       dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                       string voutype = dr["VoucherType"].ToString();
                       if (voutype == FixAccounts.VoucherTypeForCreditNoteStock || voutype == FixAccounts.VoucherTypeForStockIN)
                       {
                           dgvrow.Cells["Col_QuantityCN"].Value = dr["Quantity"].ToString();
                           dgvrow.Cells["Col_ScmQuantityCN"].Value = dr["SchemeQuantity"].ToString();
                           if (dr["Quantity"] != DBNull.Value)
                               _MTotalCN += Convert.ToInt32(dr["Quantity"].ToString());
                           if (dr["SchemeQuantity"] != DBNull.Value)
                               _MTotalScmCN  += Convert.ToInt32(dr["SchemeQuantity"].ToString());
                       }
                       else
                       {
                           dgvrow.Cells["Col_QuantityDN"].Value = dr["Quantity"].ToString();
                           dgvrow.Cells["Col_ScmQuantityDN"].Value = dr["SchemeQuantity"].ToString();
                           if (dr["Quantity"] != DBNull.Value)
                               _MTotalDN += Convert.ToInt32(dr["Quantity"].ToString());
                           if (dr["SchemeQuantity"] != DBNull.Value)
                               _MTotalScmDN += Convert.ToInt32(dr["SchemeQuantity"].ToString());
                       }                        
                        dgvrow.Cells["Col_AccountID"].Value = dr["AccountID"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        dgvrow.Cells["Col_Address"].Value = dr["AccAddress1"].ToString();
                        dgvrow.Cells["Col_ProdID"].Value = dr["ProductID"].ToString();
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
            dgvrow.Cells["Col_VoucherDate"].Value = "Total";
            dgvrow.Cells["Col_QuantityCN"].Value = _MTotalCN.ToString();
            dgvrow.Cells["Col_ScmQuantityCN"].Value = _MTotalScmCN.ToString();
            dgvrow.Cells["Col_QuantityDN"].Value = _MTotalDN.ToString();
            dgvrow.Cells["Col_ScmQuantityDN"].Value = _MTotalScmDN.ToString();
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
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                    _BindingSource = _CreditDebitNote.GetDebitCreditListProduct(mcbProduct.SelectedID, _MFromDate, _MToDate);
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[5] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName" };
                mcbProduct.ColumnWidth = new string[5] { "0", "200", "50", "50", "50" };
                mcbProduct.ValueColumnNo = 0;
                mcbProduct.UserControlToShow = new UclProduct();
                DataTable dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetOverviewData();
                mcbProduct.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        # endregion Other Private methods

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
                    if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                        txtViewtext.Text = mcbProduct.SeletedItem.ItemData[1] + " " + mcbProduct.SeletedItem.ItemData[2] + " " + mcbProduct.SeletedItem.ItemData[3];
                    else
                        txtViewtext.Text = "";
                    lblFooterMessage.Text = "";                    
                    ViewFromDate.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
                    ViewToDate.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
                    FillReportGrid();
                    PrintReportHead = "DEBIT/CREDIT NOTE LIST [PRODUCT] From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
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

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelection_KeyDown(object sender, KeyEventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

      
        # endregion Events

        #region AddToolTip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion AddToolTip
    }
}

