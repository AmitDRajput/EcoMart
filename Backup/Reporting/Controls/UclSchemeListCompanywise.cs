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
    public partial class UclSchemeListCompanywise : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;
        private List<DataGridViewRow> rowCollection;
        private Scheme _Scheme;
        private Company _Company;
        private string _MFromDate;
        private string _MToDate;      
        #endregion

        # region Constructor
        public UclSchemeListCompanywise()
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

        public override void SetFocus()
        {
            base.SetFocus();
            txtSearch.Focus();
        }

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Scheme = new Scheme();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "SCHEME LIST-COMPANYWISE";
                ConstructReportColumns();
                //FillCompanyCombo();
                //FillReportData();
                //FillReportGrid();
               
               
                ConstructReportColumns();
                ConstructSelectedGridColumns();
                ClearControls();
                pnlMultiSelection.Visible = true;
                if (dgvMultiSelection.Rows.Count > 0)
                    dgvMultiSelection.Rows.Clear();
                FillMultiSelectionGrid();
                txtNoofSearches.Enabled = false;
                dgvSelected.Visible = false;
                btnOKMultiSelection.Visible = false;
                AddToolTip();
                cbSelectAll.Checked = false;
                cbSelectAll.Visible = false;
                tsbtnPrint.Enabled = false;
                txtSearch.Focus();
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
                pnlMultiSelection.Visible = true;
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
                if (pnlMultiSelection.Visible == true)
                {
                    pnlMultiSelection.Focus();
                    txtSearch.Focus();
                    retValue = true;
                }

            }
            if (keyPressed == Keys.G && modifier == Keys.Alt)
            {
                if (pnlMultiSelection.Visible == true)
                {
                    btnOKMultiSelectionClick();
                    retValue = true;
                }
            }
            if (keyPressed == Keys.O && modifier == Keys.Alt)
            {
                if (btnOK.Visible == true)
                {
                    btnOKClick();
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

        public DataTable GetReportData()
        {
            return _BindingSource.Copy();
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
                PrintReportHead = "Scheme List [Company]";
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    int minteger = 0;
                    if (dr.Cells["Col_ID"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
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

                        int length = Math.Min(dr.Cells["Col_ProdName"].Value.ToString().Length, 20);
                        row = new PrintRow((dr.Cells["Col_ProdName"].Value.ToString()).Substring(0, length), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_UOM"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, 200, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Pack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, 240, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Quantity1"].Value != null)
                        {
                            minteger = Convert.ToInt32(dr.Cells["Col_Quantity1"].Value.ToString());
                            if (minteger > 0)
                                row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 320, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Scheme1"].Value != null)
                        {
                            minteger = Convert.ToInt32(dr.Cells["Col_Scheme1"].Value.ToString());
                            if (minteger > 0)
                                row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 370, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Quantity2"].Value != null)
                        {
                            minteger = Convert.ToInt32(dr.Cells["Col_Quantity2"].Value.ToString());
                            if (minteger > 0)
                                row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 420, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Scheme2"].Value != null)
                        {
                            minteger = Convert.ToInt32(dr.Cells["Col_Scheme2"].Value.ToString());
                            if (minteger > 0)
                                row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 470, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Quantity3"].Value != null)
                        {
                            minteger = Convert.ToInt32(dr.Cells["Col_Quantity3"].Value.ToString());
                            if (minteger > 0)
                                row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 520, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Scheme3"].Value != null)
                        {
                            minteger = Convert.ToInt32(dr.Cells["Col_Scheme3"].Value.ToString());
                            if (minteger > 0)
                                row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 570, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_StartDate"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_StartDate"].Value.ToString(), PrintRowPixel, 620, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ClosingDate"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ClosingDate"].Value.ToString(), PrintRowPixel, 700, PrintFont);
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Product", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("UOM", PrintRowPixel, 190, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 240, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 310, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM", PrintRowPixel, 360, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 410, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM", PrintRowPixel, 460, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 510, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM", PrintRowPixel, 560, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Start Date", PrintRowPixel, 620, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("End Date", PrintRowPixel, 700, PrintFont);
                PrintBill.Rows.Add(row);


                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                // PrintRowPixel += 17;

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 7;
            return PrintRowCount;
        }


        #endregion

        #region Other Private methods
        public void ClearControls()
        {
            try
            {
                lblFooterMessage.Text = "";
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
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdID";
                column.DataPropertyName = "ProductID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 210;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 80;
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
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity1";
                column.DataPropertyName = "ProductQuantity1";
                column.HeaderText = "Qty-1";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme1";
                column.DataPropertyName = "SchemeQuantity1";
                column.HeaderText = "Scm-1";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity2";
                column.DataPropertyName = "ProductQuantity2";
                column.HeaderText = "Qty-2";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme2";
                column.DataPropertyName = "SchemeQuantity2";
                column.HeaderText = "Scm-2";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity3";
                column.DataPropertyName = "ProductQuantity3";
                column.HeaderText = "Qty-3";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme3";
                column.DataPropertyName = "SchemeQuantity3";
                column.HeaderText = "Scm-3";
                column.Width = 60;
                dgvReportList.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StartDate";
                column.DataPropertyName = "StartingDate";
                column.HeaderText = "StartDate";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingDate";
                column.DataPropertyName = "ClosingDate";
                column.HeaderText = "EndDate";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_compId";
                column.DataPropertyName = "ProdCompID";
                column.HeaderText = "compid";
                column.Width = 80;
                column.Visible = false;
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
                column.DataPropertyName = "CompId";
                column.ValueType = typeof(string);
                column.HeaderText = "ID";
                column.Visible = false;
                dgvMultiSelection.Columns.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
              //  columnCheck.DataPropertyName = "TAG";
                columnCheck.HeaderText = "Check";
                column.ValueType = typeof(bool);
                columnCheck.Width = 100;
                dgvMultiSelection.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
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

        private void  BindReportData(string ID, string party)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.Cells["Col_ID"].Value = ID;
                currentdr.Cells["Col_ProdName"].Value = party;
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["ProdCompID"].ToString() == ID)
                    {
                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ProductID"].ToString();
                        currentdr.Cells["Col_ProdID"].Value = dr["ProductID"].ToString();
                        currentdr.Cells["Col_ProdName"].Value = dr["ProdName"].ToString();
                        currentdr.Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                        currentdr.Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                        currentdr.Cells["Col_Comp"].Value = dr["ProdCompShortName"].ToString();
                        currentdr.Cells["Col_Quantity1"].Value = dr["ProductQuantity1"].ToString();
                        currentdr.Cells["Col_Scheme1"].Value = dr["SchemeQuantity1"].ToString();
                        currentdr.Cells["Col_Quantity2"].Value = dr["ProductQuantity2"].ToString();
                        currentdr.Cells["Col_Scheme2"].Value = dr["SchemeQuantity2"].ToString();
                        currentdr.Cells["Col_Quantity3"].Value = dr["ProductQuantity3"].ToString();
                        currentdr.Cells["Col_Scheme3"].Value = dr["SchemeQuantity3"].ToString();
                        currentdr.Cells["Col_StartDate"].Value = General.GetDateInShortDateFormat(dr["StartingDate"].ToString());
                        currentdr.Cells["Col_ClosingDate"].Value = General.GetDateInShortDateFormat(dr["ClosingDate"].ToString());
                        currentdr.Cells["Col_compId"].Value = ID;
                        
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
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }       

        private void CheckFiltertxtSearch(string txtString)
        {
            try
            {
                _BindingSourceMultiSelection.DefaultView.RowFilter = "CompName like '" + txtString + "%'";

            }
            catch (Exception ex) { Log.WriteException(ex); }

        }       

        private void NoofRowsSelected()
        {
            int noofrecords = dgvSelected.Rows.Count;
            try
            {
                if (noofrecords == 0)
                    txtNoofSearches.Text = "NO Records ";
                else if (noofrecords == 1)
                    txtNoofSearches.Text = "Record : " + noofrecords.ToString().Trim();
                else
                    txtNoofSearches.Text = "Records : " + noofrecords.ToString().Trim();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void FillReportGrid()
        //{
        //    try
        //    {
        //        dgvReportList.DataSource = _BindingSource;
        //        dgvReportList.Bind();
        //        CheckFilter();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}
        //private void CheckFilter()
        //{
            
        //    try
        //    {                
        //        if (mcbCompany.SelectedID != null && mcbCompany.SelectedID != "" )
        //           _BindingSource.DefaultView.RowFilter = "ProdCompID = '" + mcbCompany.SelectedID +  "'";                 
        //        NoofRows();
        //    }
        //    catch (Exception ex) { Log.WriteException(ex); }

        //}

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

        private void NoofSearchesSelected()
        {
            int i = 0;
            bool iftrue = false;
            if (dgvSelected.Rows.Count > 0)
                dgvSelected.Rows.Clear();
            _BindingSourceMultiSelection.DefaultView.RowFilter = "";

            try
            {
                foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                {
                    if (drow.Cells["Col_Check"].Value != DBNull.Value && drow.Cells["Col_Check"].Value != null)
                        iftrue = Convert.ToBoolean(drow.Cells["Col_Check"].Value);
                    else
                        iftrue = false;
                    if (iftrue)
                    {
                        int selectedrowindex = dgvSelected.Rows.Add();
                        dgvSelected.Rows[selectedrowindex].Cells["Col_ID"].Value = drow.Cells["Col_ID"].Value.ToString();
                        dgvSelected.Rows[selectedrowindex].Cells["Col_Name"].Value = drow.Cells["Col_Name"].Value.ToString();
                        dgvSelected.Rows[selectedrowindex].Cells["Col_Address"].Value = drow.Cells["Col_Address"].Value.ToString();
                        i += 1;
                    }
                }
                txtNoofSearches.Enabled = true;
                txtNoofSearches.Text = i.ToString("#0");
                txtNoofSearches.Enabled = false;
                txtSearch.Text = "";
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
        }
        private void btnOKMultiSelectionClick()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ConstructReportColumns();
                tsbtnPrint.Enabled = true;
                pnlMultiSelection.Visible = false;
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
                             GetSchemesCompanywise(mcompID);
                            if (_BindingSource.Rows.Count > 0)
                            {
                                BindReportData(row.Cells["Col_ID"].Value.ToString(), row.Cells["Col_Name"].Value.ToString());
                            }
                        }
                    }
                }
                this.Cursor = Cursors.Default;
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void GetSchemesCompanywise(string compID)
        {
            DataTable dtable = new DataTable();
            dtable = _Scheme.GetOverviewDataForSelectedCompany(compID);
            _BindingSource = dtable;
        }
        //private void FillReportData()
        //{
        //    try
        //    {
        //        DataTable dtable = new DataTable();
        //        dtable = _Scheme.GetOverviewData();
        //        _BindingSource = dtable;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void FillCompanyCombo()
        //{
        //    try
        //    {
        //        mcbCompany.SelectedID = null;
        //        mcbCompany.SourceDataString = new string[3] { "CompID", "CompName", "CompShortName" };
        //        mcbCompany.ColumnWidth = new string[3] { "0", "250", "50" };
        //        mcbCompany.ValueColumnNo = 0;
        //        mcbCompany.UserControlToShow = new UclCompany();
        //        Company _Company = new Company();
        //        DataTable dtable = _Company.GetOverviewData();
        //        mcbCompany.FillData(dtable);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void FillMultiSelectionGrid()
        //{
        //    try
        //    {
        //        ConstructdgvMultiSelectionColumns();
        //        FillMultiSelectionData();
        //        dgvMultiSelection.DataSource = _BindingSourceCompany;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void FillMultiSelectionData()
        //{
        //    try
        //    {
        //        DataTable dtable = new DataTable();
        //        _Company = new Company();
        //        dtable = _Company.CreateTempComp();
        //        _BindingSourceCompany = dtable;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}
        #endregion

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearch.Text != null && txtSearch.Text.ToString() != "")
            {
                CheckFiltertxtSearch(txtSearch.Text.ToString().Trim());
                btnOK.Visible = true;
                btnOKMultiSelection.Visible = false;
                cbSelectAll.Visible = true;
                cbSelectAll.Checked = false;
                dgvMultiSelection.Focus();
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            btnOKClick();
        }
        private void btnOKClick()
        {
            NoofSearchesSelected();
            btnOKMultiSelection.Visible = true;
            cbSelectAll.Visible = false;
            txtSearch.Text = "";
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
      
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttSchemeCompany.SetToolTip(txtSearch, "Type First Few characters Press Enter");
                ttSchemeCompany.SetToolTip(cbSelectAll, "Check to Select All Companies in the List and Click OK");
                ttSchemeCompany.SetToolTip(btnViewList, "Click to View List of Selected Companies and Click Again to Close the List");
                ttSchemeCompany.SetToolTip(btnOK, "Click to Save the Seleted Products into the List");
                ttSchemeCompany.SetToolTip(btnOKMultiSelection, "Click to See Report = F10");
                ttSchemeCompany.SetToolTip(pnlMultiSelection, "F12 to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion
    }
}
