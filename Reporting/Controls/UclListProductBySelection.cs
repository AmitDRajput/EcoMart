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
    public partial class UclListProductBySelection : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;
        public DataTable selectedcompanies;      
        private Product _Product;
        private Company _Company;
        private Shelf _Shelf;
        private List<DataGridViewRow> rowCollection;
        private int selectedRowCount;
        # endregion Declaration

        # region Constructor
        public UclListProductBySelection()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclProduct();
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
                selectedRowCount = 0;
                _BindingSource = new DataTable();
                _Product = new Product();
                _Company = new Company();
                _Shelf = new Shelf();
                rowCollection = new List<DataGridViewRow>();
                headerLabel1.Text = "PRODUCT LIST BY SELECTION";
                InitializeReportGrid();
                ConstructSelectedGridColumns();
                ClearControls();
                HidepnlGO();
                dgvSelected.Visible = false;             
                txtNoofSearches.Enabled = false;
                btnOKMultiSelection1.Visible = true;
                AddToolTip();
                PrintReportHead = "Product List [All]";
                PrintReportHead2 = "";
                cbSelectAll.Checked = false;
                cbSelectAll.Visible = false;
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
                pnlMultiSelection1.Visible = true;
                tsbtnPrint.Enabled = false;
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
            txtSearch.Focus();
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
                double mamt = 0;
                double mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                string iffirst = "Y";
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_ID"].Value != null || dr.Cells["Col_ID"].Value.ToString() != "")
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
                        PrintColumnPixel = 1;
                        if (dr.Cells["Col_ProductLoosePack"].Value == null || dr.Cells["Col_ProductLoosePack"].Value.ToString() == "")
                        {
                            PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
                            if (iffirst == "Y")
                            {
                                iffirst = "N";
                            }
                            else
                            {
                                {
                                    PrintRowPixel += 17;
                                    PrintRowCount += 1;                                    
                                }
                            }
                        }
                       
                        if (dr.Cells["Col_Name"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Name"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));


                        PrintColumnPixel += 150;
                        if (dr.Cells["Col_ProductLoosePack"].Visible == true && dr.Cells["Col_ProductLoosePack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductLoosePack"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        PrintColumnPixel += 50;
                        if (dr.Cells["Col_ProductPack"].Visible == true && dr.Cells["Col_ProductPack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductPack"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        PrintColumnPixel += 70;
                        if (dr.Cells["Col_CompanyShortName"].Visible == true && dr.Cells["Col_CompanyShortName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompanyShortName"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        PrintColumnPixel += 200;
                        if (dr.Cells["Col_MRP"].Visible == true && dr.Cells["Col_MRP"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 75;

                        }
                        if (dr.Cells["Col_PurchaseRate"].Visible == true && dr.Cells["Col_PurchaseRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 75;
                        }
                        if (dr.Cells["Col_SaleRate"].Visible == true && dr.Cells["Col_SaleRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 75;
                        }
                        if (dr.Cells["Col_VATPercent"].Visible == true && dr.Cells["Col_VATPercent"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_VATPercent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((6.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 40;
                        }
                        if (dr.Cells["Col_ShelfCode"].Visible == true && dr.Cells["Col_ShelfCode"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ShelfCode"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
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
                PrintColumnPixel = 1;

                row = new PrintRow("Product", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 150;
                row = new PrintRow("UOM", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 50;
                row = new PrintRow("Pack", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 70;
                row = new PrintRow("Company", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 240;

                if (dgvReportList.Columns["Col_MRP"].Visible == true)
                {
                    row = new PrintRow("MRP", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 70;
                }
                if (dgvReportList.Columns["Col_PurchaseRate"].Visible == true)
                {
                    row = new PrintRow("Pur.Rate", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 60;
                }
                if (dgvReportList.Columns["Col_SaleRate"].Visible == true)
                {
                    row = new PrintRow("Sale Rate", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 55;
                }
                if (dgvReportList.Columns["Col_VATPercent"].Visible == true)
                {
                    row = new PrintRow("VAT%", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 40;
                }
                if (dgvReportList.Columns["Col_ShelfCode"].Visible == true)
                {
                    row = new PrintRow("Shelf", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                }

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


      
        #endregion Print Report

        # region Other Private methods

        public void ClearControls()
        {
            try
            {
                lblFooterMessage.Text = "";
                InitializeReportGrid();
                rbtnCompany.Checked = true;
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
                column.DataPropertyName = "ProductId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyShortName";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Company";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "ProdLastPurchaseMRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "ProdLastPurchaseRate";
                column.HeaderText = "PUR.RATE";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "ProdLastPurchaseSaleRate";
                column.HeaderText = "SALE.RATE";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfCode";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructMultiSelectionGridColumns()
        {
            try
            {
                dgvMultiSelection.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvMultiSelection.Columns.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                //columnCheck.DataPropertyName = "TAG";
                columnCheck.HeaderText = "Check";
                column.ValueType = typeof(bool);
                columnCheck.Width = 100;
                dgvMultiSelection.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "Name";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructSelectedGridColumns()
        {
            try
            {
                dgvSelected.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "Name";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvSelected.Columns.Add(column);

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
            txtSearch.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            dgvReportList.Focus();
        }

        private void BindReportGrid(string ID, string CompName)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                currentdr.Cells["Col_ID"].Value = ID;
                currentdr.Cells["Col_Name"].Value = CompName;

                foreach (DataRow dr in _BindingSource.Rows)
                { 
                    if ((dr["ProdCompID"].ToString() == ID && rbtnCompany.Checked == true) || (rbtnShelf.Checked == true && dr["ProdShelfID"].ToString() == ID))
                    {

                        double mamt = 0;

                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ProductID"].ToString();
                        currentdr.Cells["Col_Name"].Value = dr["ProdName"].ToString();
                        currentdr.Cells["Col_ProductLoosePack"].Value = dr["ProdLoosePack"].ToString();
                        currentdr.Cells["Col_ProductPack"].Value = dr["ProdPack"].ToString();
                        currentdr.Cells["Col_CompanyShortName"].Value = dr["ProdCompShortName"].ToString();
                        currentdr.Cells["Col_MRP"].Value = dr["ProdLastPurchaseMRP"].ToString();
                        currentdr.Cells["Col_PurchaseRate"].Value = dr["ProdLastPurchaseRate"].ToString();
                        if (dr["ProdLastPurchaseSaleRate"] != DBNull.Value)
                            mamt = Convert.ToDouble(dr["ProdLastPurchaseSaleRate"].ToString());
                        currentdr.Cells["Col_SaleRate"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["ProdVATPercent"].ToString());
                        currentdr.Cells["Col_VATPercent"].Value = mamt.ToString("#0.00");
                        currentdr.Cells["Col_ShelfCode"].Value = dr["ShelfCode"].ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }
        private void CheckFiltertxtSearch(string txtString)
        {

            try
            {
                _BindingSourceMultiSelection.DefaultView.RowFilter = "Name like '" + txtString + "%'";
            }
            catch (Exception ex) { Log.WriteException(ex); }
        }

        private void FillMultiSelectionGrid()
        {

            try
            {
                selectedRowCount = 0;
                RefreshSelectedRowCounter();
                dgvMultiSelection.DataSource = null;
                ConstructMultiSelectionGridColumns();
                FillMultiSelectionData();
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillMultiSelectionData()
        {
            try
            {
                DataTable dtable = new DataTable();
                _Product = new Product();
                if (rbtnCompany.Checked == true)
                    dtable = _Company.GetOverviewDataForMultiSelection();
                else if (rbtnShelf.Checked == true)
                    dtable = _Shelf.GetOverviewDataForMultiSelection();
                else dtable = null;
                _BindingSourceMultiSelection = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnViewListClick()
        {
            dgvSelected.Sort(dgvSelected.Columns["Col_Name"], ListSortDirection.Ascending);
            if (dgvSelected.Visible == false)
            {
                dgvSelected.Visible = true;
                dgvMultiSelection.Enabled = false;
                btnViewList.Text = "Close";
            }
            else
            {
                dgvSelected.Visible = false;
                dgvMultiSelection.Enabled = true;
                btnViewList.Text = "View";
            }
        }
        private void GetProductsCompanywise(string compID)
        {
            DataTable dtable = new DataTable();
            dtable = _Product.GetOverviewDataForCompany(compID);
            _BindingSource = dtable;
        }
        private void GetProductsShelfwise(string shelfID)
        {
            DataTable dtable = new DataTable();
            dtable = _Product.GetOverviewDataForShelf(shelfID);
            _BindingSource = dtable;
        }
        private void RefreshSelectedRowCounter()
        {
            selectedRowCount = dgvSelected.Rows.Count;
            txtNoofSearches.Text = selectedRowCount.ToString("#0");
        }
        private void AddIndgvSelectedGrid(DataGridViewRow drow)
        {
            bool iffound = false;
            foreach (DataGridViewRow drowselected in dgvSelected.Rows)
            {
                if (drowselected.Cells["Col_ID"].Value.ToString() == drow.Cells["Col_ID"].Value.ToString())
                {
                    iffound = true;
                    break;
                }
            }
            if (!iffound)
            {

                int selectedrowindex = dgvSelected.Rows.Add();
                dgvSelected.Rows[selectedrowindex].Cells["Col_ID"].Value = drow.Cells["Col_ID"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Name"].Value = drow.Cells["Col_Name"].Value.ToString();
            }

        }

        private void RemoveFromdgvSelectedGrid(DataGridViewRow drow)
        {
            foreach (DataGridViewRow drowselected in dgvSelected.Rows)
            {
                if (drowselected.Cells["Col_ID"].Value.ToString() == drow.Cells["Col_ID"].Value.ToString())
                {
                    dgvSelected.Rows.Remove(drowselected);
                    break;
                }
            }


        }

        #endregion  Other Private Methods

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void FormatReportGrid()
        {
            dgvReportList.DoubleColumnNames.Add("Col_MRP");
            dgvReportList.DoubleColumnNames.Add("Col_PurchaseRate");
            dgvReportList.DoubleColumnNames.Add("Col_SaleRate");
            dgvReportList.DoubleColumnNames.Add("Col_VATPercent");
            dgvReportList.OptionalColumnNames.Add("Col_MRP");
            dgvReportList.OptionalColumnNames.Add("Col_PurchaseRate");
            dgvReportList.OptionalColumnNames.Add("Col_SaleRate");
            dgvReportList.OptionalColumnNames.Add("Col_VATPercent");
            dgvReportList.OptionalColumnNames.Add("Col_ShelfCode");
        }

        private void btnOKMultiSelectionClick()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                InitializeReportGrid();
                FormatReportGrid();
                ShowpnlGO();
                rowCollection = new List<DataGridViewRow>();
                dgvSelected.Sort(dgvSelected.Columns["Col_Name"], ListSortDirection.Ascending);
                foreach (DataGridViewRow selectedrow in dgvSelected.Rows)
                {
                    rowCollection.Add(selectedrow);
                }

                if (rowCollection.Count > 0)
                {

                    string mcompID = "";
                    string mshelfcode = "";
                    foreach (DataGridViewRow row in rowCollection)
                    {
                        mcompID = "";
                        mshelfcode = "";
                        if (rbtnCompany.Checked == true)
                        {
                            if (row.Cells["Col_ID"].Value != null)
                            {
                                mcompID = row.Cells["Col_ID"].Value.ToString();
                            }
                            if (mcompID != "")
                            {
                                GetProductsCompanywise(mcompID);
                            }
                        }
                        else if (rbtnShelf.Checked == true)
                        {
                            if (row.Cells["Col_ID"].Value != null)
                            {
                                mshelfcode = row.Cells["Col_ID"].Value.ToString();
                            }
                            if (mshelfcode != "")
                            {
                                GetProductsShelfwise(mshelfcode);
                            }
                        }                       
                        if (_BindingSource.Rows.Count > 0)
                        {
                            BindReportGrid(row.Cells["Col_ID"].Value.ToString(), row.Cells["Col_Name"].Value.ToString());
                        }

                    }
                }
                FormatReportGrid();
                this.Cursor = Cursors.Default;
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int mlen = 0;
                mlen = txtSearch.Text.ToString().Length;
                if (txtSearch.Text != null && txtSearch.Text != "")
                {
                    if (cbSelectAll.Checked == true)
                    {
                        if (dgvMultiSelection.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                            {

                                if (drow.Cells["Col_Name"].Value != null && txtSearch.Text.ToString() == drow.Cells["Col_Name"].Value.ToString().Substring(0, mlen))
                                {
                                    drow.Cells["Col_Check"].Value = cbSelectAll.Checked;
                                }
                            }
                        }
                        txtSearch.Text = "";
                    }

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgvMultiSelection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMultiSelection.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvMultiSelection_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvMultiSelection.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if (Convert.ToBoolean(dgvMultiSelection.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true)
                    {
                        selectedRowCount++;
                        rowCollection.Add(dgvMultiSelection.Rows[e.RowIndex]);
                        AddIndgvSelectedGrid(dgvMultiSelection.Rows[e.RowIndex]);
                    }
                    else
                    {
                        selectedRowCount--;
                        RemoveFromdgvSelectedGrid(dgvMultiSelection.Rows[e.RowIndex]);
                    }
                    RefreshSelectedRowCounter();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearch.Text != null && txtSearch.Text.ToString() != "")
            {
                CheckFiltertxtSearch(txtSearch.Text.ToString().Trim());
                cbSelectAll.Visible = true;
                cbSelectAll.Checked = false;
                dgvMultiSelection.Focus();
            }
        }

        private void btnViewList_KeyDown(object sender, KeyEventArgs e)
        {
            btnViewListClick();
        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            btnViewListClick();
        }
        private void rbtnCompany_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChange();
        }

        private void CheckedChange()
        {
            if (rbtnCompany.Checked == true || rbtnShelf.Checked == true)
            FillMultiSelectionGrid();           
        }
        #endregion  Events

        #region ToolTip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(txtSearch, "Type First Few characters of Product Name and Press Enter");
                ttToolTip.SetToolTip(cbSelectAll, "Check to Select All Products in the List and Click OK");
                ttToolTip.SetToolTip(btnViewList, "Click to View List of Selected Products and Click Again to Close the List");
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion  ToolTip
    }
}
