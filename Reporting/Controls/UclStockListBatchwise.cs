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
    public partial class UclStockListBatchwise : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;
        private List<DataGridViewRow> rowCollection;
        private Company _Company;
        private SsStock _SsStock;       
        private int selectedRowCount;
        # endregion

        # region Constructor
        public UclStockListBatchwise()
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
        # endregion

        #region IOverview Members

        public override void SetFocus()
        {           
            mcbCompany.Focus();
        }

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _SsStock = new SsStock();               
                headerLabel1.Text = "STOCK-CURRENT STOCK BATCHWISE";
              
                cbSelectAll.Checked = false;
                ConstructReportColumns();
                ConstructSelectedGridColumns();
                ClearControls();
                rbtClosingStock.Checked = true;
                FillCompanyCombo();
                pnlMultiSelection1.Visible = true;
                if (dgvMultiSelection.Rows.Count > 0)
                    dgvMultiSelection.Rows.Clear();
                FillMultiSelectionGrid();
                txtNoofSearches.Enabled = false;
                dgvSelected.Visible = false;
                cbSelectAll.Checked = false;
                cbSelectAll.Visible = false;
                tsbtnPrint.Enabled = false;
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
            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                if (pnlMultiSelection1.Visible == true)
                {
                    pnlMultiSelection1.Focus();
                    txtSearch.Focus();
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
      
        #endregion IOverView

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
                totalrows = CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                PrintIfFirstRow = "Y";
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
                    }
                    PrintRowPixel += 17;
                    PrintRowCount += 1;
                    double mamt = 0;

                    if (dr.Cells["Col_ProductName"].Value != null && dr.Cells["Col_ProductLoosePack"].Value == null)
                    {
                        if (PrintIfFirstRow != "Y")
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                            row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintIfFirstRow = "N";
                        }
                        else
                        {
                            row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintIfFirstRow = "N";
                        }
                    }
                    else
                    {
                        if (dr.Cells["Col_ProductName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProductLoosePack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductLoosePack"].Value.ToString().PadRight(15), PrintRowPixel, 250, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProductPack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductPack"].Value.ToString().PadLeft(6), PrintRowPixel, 300, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Batch"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadLeft(6), PrintRowPixel, 400, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        mamt = 0;
                        if (dr.Cells["Col_MRP"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(500.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                      
                        if (rbtClosingStock.Checked == true)
                        {
                            mamt = 0;
                            if (dr.Cells["Col_ClosingStock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_ClosingStock"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(600.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            if (dr.Cells["Col_ProductPack"].Value != null)
                            {
                                row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                        }
                        else
                        {
                            mamt = 0;
                            if (dr.Cells["Col_OpeningStock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_OpeningStock"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(400.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            if (dr.Cells["Col_ProductPack"].Value != null)
                            {
                                row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                        }
                    }
                }
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Product", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("UOM", PrintRowPixel, 250, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 300, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 400, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("MRP", PrintRowPixel, 520, PrintFont);
                PrintBill.Rows.Add(row);
                if (rbtClosingStock.Checked)
                {
                    row = new PrintRow("Cl Stock", PrintRowPixel, 620, PrintFont);
                    PrintBill.Rows.Add(row);
                }
                else
                {
                    row = new PrintRow("OP Stock", PrintRowPixel, 620, PrintFont);
                    PrintBill.Rows.Add(row);
                }
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

        #endregion IReportMember

        #region Other Private methods
        public void ClearControls()
        {
            try
            {
                mcbCompany.SelectedID = "";
                FillCompanyCombo();
                lblFooterMessage.Text = "";
                rowCollection = new List<DataGridViewRow>();
                ClearGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void ClearGrid()
        {
            if (dgvReportList.Rows.Count > 0)
            {
                DataTable dtable = new DataTable();
                _BindingSource = dtable;
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
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 270;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 85;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 110;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Company";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "CL Stock";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                if (rbtClosingStock.Checked == true)
                    column.Visible = true;
                else
                    column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpeningStock";
                column.DataPropertyName = "OpeningStock";
                column.HeaderText = "OP Stock";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                if (rbtOpeningSTock.Checked == true)
                    column.Visible = true;
                else
                    column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfCode";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf Code";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.Visible = false;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        # endregion

        #region Other Private Methods

        private void ConstructMultiSelectionGridColumns()
        {
            try
            {
                dgvMultiSelection.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "CompID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvMultiSelection.Columns.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
             //   columnCheck.DataPropertyName = "TAG";
                columnCheck.HeaderText = "Check";
                column.ValueType = typeof(bool);
                columnCheck.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvMultiSelection.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 400;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "CompShortName";
                column.HeaderText = "Address";
                column.Visible = false;
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Contact";
                column.DataPropertyName = "CompContactPerson";
                column.HeaderText = "Opening Debit";
                column.Visible = false;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_C";
                column.DataPropertyName = "Tag";
                column.Visible = false;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Co";
                column.DataPropertyName = "PartyID_1";
                //  column.HeaderText = "Opening Debit";
                column.Visible = false;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Con";
                column.DataPropertyName = "PartyID_2";
                //  column.HeaderText = "Opening Debit";
                column.Visible = false;
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
                column.DataPropertyName = "AccountID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
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


        private void BindReportGrid(string AccID, string party, string narr, double opDebit, double opCredit)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.Cells["Col_ID"].Value = AccID;
                currentdr.Cells["Col_ProductName"].Value = party;
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["ProdCompID"].ToString() == AccID)
                    {
                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ProductID"].ToString();
                        currentdr.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                        currentdr.Cells["Col_ProductLoosePack"].Value = dr["ProdLoosePack"].ToString();
                        currentdr.Cells["Col_ProductPack"].Value = dr["ProdPack"].ToString();
                        currentdr.Cells["Col_CompanyShortName"].Value = dr["ProdCompShortName"].ToString();
                        currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                        currentdr.Cells["Col_MRP"].Value = dr["MRP"].ToString();
                        currentdr.Cells["Col_ClosingStock"].Value = dr["ClosingStock"].ToString();
                        currentdr.Cells["Col_OpeningStock"].Value = dr["OpeningStock"].ToString();
                        currentdr.Cells["Col_ShelfCode"].Value = dr["ShelfCode"].ToString();

                    }
                }
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            NoofRows();
        }

        private void CheckFiltertxtSearch(string txtString)
        {
            try
            {
                _BindingSourceMultiSelection.DefaultView.RowFilter = "CompName like '" + txtString + "%'";

            }
            catch (Exception ex) { Log.WriteException(ex); }

        }

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }       

        //private void NoofRowsSelected()
        //{
        //    int noofrecords = dgvSelected.Rows.Count;
        //    try
        //    {
        //        if (noofrecords == 0)
        //            txtNoofSearches.Text = "NO Records ";
        //        else if (noofrecords == 1)
        //            txtNoofSearches.Text = "Record : " + noofrecords.ToString().Trim();
        //        else
        //            txtNoofSearches.Text = "Records : " + noofrecords.ToString().Trim();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}
        private void RefreshSelectedRowCounter()
        {
            selectedRowCount = dgvSelected.Rows.Count;
            txtNoofSearches.Text = selectedRowCount.ToString("#0");
        }
        private void GetStockDataProductwise(string compID)
        {
            DataTable dtable = new DataTable();
          
                if (rbtOpeningSTock.Checked == true)
                    dtable = _SsStock.GetStockForAllBatchOpening(compID);
                else
                    dtable = _SsStock.GetStockForAllBatchClosing(compID);
           
            _BindingSource = dtable;
        }
        private void FillMultiSelectionGrid()
        {

            try
            {
                ConstructMultiSelectionGridColumns();
                if (dgvMultiSelection.Rows.Count > 0)
                    dgvMultiSelection.Rows.Clear();
                FillMultiSelectionData();
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillCompanyCombo()
        {
            try
            {
                mcbCompany.SelectedID = null;
                mcbCompany.SourceDataString = new string[2] { "CompID", "CompName" };
                mcbCompany.ColumnWidth = new string[2] { "0", "250" };
                mcbCompany.ValueColumnNo = 0;
                mcbCompany.UserControlToShow = new UclCompany();
                Company _Company = new Company();
                DataTable dtable = _Company.GetOverviewData();
                mcbCompany.FillData(dtable);
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
                _Company = new Company();
                dtable = _Company.GetOverviewData();
                _BindingSourceMultiSelection = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

       
        private void btnOKMultiSelectionClick()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ConstructReportColumns();
                if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
                {
                    tsbtnPrint.Enabled = true;
                }       
                pnlMultiSelection1.Visible = false;
                if (dgvReportList.Rows.Count > 0)
                    dgvReportList.Rows.Clear();
                rowCollection = new List<DataGridViewRow>();

                foreach (DataGridViewRow selectedrow in dgvSelected.Rows)
                {
                    rowCollection.Add(selectedrow);
                }
                if (rowCollection.Count > 0)
                {
                    if (dgvReportList.Rows.Count > 0)
                        dgvReportList.Rows.Clear();

                    string mcompID = "";
                    foreach (DataGridViewRow row in rowCollection)
                    {
                        mcompID = "";
                        if (row.Cells["Col_ID"].Value != null)
                            mcompID = row.Cells["Col_ID"].Value.ToString();
                        if (mcompID != "")
                        {
                            GetStockDataProductwise(mcompID);
                            if (_BindingSource.Rows.Count > 0)
                            {
                                BindReportGrid(row.Cells["Col_ID"].Value.ToString(), row.Cells["Col_Name"].Value.ToString(), "",
                                Convert.ToDouble("0.0"), Convert.ToDouble("0.0"));
                            }
                        }
                    }
                }
                else
                {
                    string mcomp = mcbCompany.SelectedID.ToString();
                    GetStockDataProductwise(mcomp);
                    if (_BindingSource.Rows.Count > 0)
                    {
                        BindReportGrid(mcbCompany.SeletedItem.ItemData[0].ToString(), mcbCompany.SeletedItem.ItemData[1].ToString(), "",
                        Convert.ToDouble("0.0"), Convert.ToDouble("0.0"));
                    }
                }
                this.Cursor = Cursors.Default;
                PrintReportHead = "Stock [Current Batch] ";
                PrintReportHead2 = "";
                NoofRows();
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
      
        #endregion OtherPrivate methods

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
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

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int mlen = 0;
                mlen = txtSearch.Text.ToString().Trim().Length;
                if (txtSearch.Text != null && txtSearch.Text != "")
                {
                    if (cbSelectAll.Checked == true)
                    {
                        if (dgvMultiSelection.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                            {

                                if (drow.Cells["Col_Name"].Value != null && txtSearch.Text.ToString().ToUpper() == drow.Cells["Col_Name"].Value.ToString().Trim().Substring(0, mlen))
                                {
                                    drow.Cells["Col_Check"].Value = cbSelectAll.Checked;
                                }
                            }
                        }
                        txtSearch.Text = "";
                        cbSelectAll.Checked = false;
                    }

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                //    dgvSelected.Rows[selectedrowindex].Cells["Col_UOM"].Value = drow.Cells["Col_UOM"].Value.ToString();
                //   dgvSelected.Rows[selectedrowindex].Cells["Col_Pack"].Value = drow.Cells["Col_Pack"].Value.ToString();
                // dgvSelected.Rows[selectedrowindex].Cells["Col_Comp"].Value = drow.Cells["Col_Comp"].Value.ToString();
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
        private void btnViewListClick()
        {
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

        private void btnViewList_Click(object sender, EventArgs e)
        {
            btnViewListClick();
        }

        //private void dgvReport_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    string voutype = "";
        //    try
        //    {
        //        if (dgvReportList.CurrentRow != null && dgvReportList.Rows.Count > 0)
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            string selectedID = dgvReportList.CurrentRow.Cells["Col_ID"].Value.ToString();
        //            voutype = dgvReportList.CurrentRow.Cells["Col_VoucherType"].Value.ToString();

        //            if (voutype == FixAccounts.VoucherTypeForCashPurchase || voutype == FixAccounts.VoucherTypeForCreditPurchase || voutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
        //                ViewControl = new UclPurchase();
        //            else if (voutype == FixAccounts.VoucherTypeForCashPayment)
        //                ViewControl = new UclCashPayment();
        //            else if (voutype == FixAccounts.VoucherTypeForBankPayment)
        //                ViewControl = new UclBankPayment();
        //            // else if (voutype == "JV ")

        //            ShowViewForm(selectedID, ViewMode.Current);
        //            this.Cursor = Cursors.Default;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError(ex.ToString());
        //    }

        //}
        private void mcbCompany_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCompany.SelectedID == null || mcbCompany.SelectedID == "")
                txtSearch.Focus();
            else
                btnOKMultiSelection1.Focus();
        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(txtSearch, "Type First Few characters Press Enter");
                ttToolTip.SetToolTip(cbSelectAll, "Check to Select All Companies in the List and Click OK");
                ttToolTip.SetToolTip(btnViewList, "Click to View List of Selected Companies and Click Again to Close the List");
               
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = F10");
                ttToolTip.SetToolTip(pnlMultiSelection1, "F12 to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion

    }
}
