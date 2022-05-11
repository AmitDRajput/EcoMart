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
using System.IO;

namespace PharmaSYSRetailPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclH1SaleList : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private Stock _Stock;
        private string _MFromDate;
        private string _MToDate;
        #endregion

        # region Constructor
        public UclH1SaleList()
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
                _Stock = new Stock();
                headerLabel1.Text = "SALE PRODUCT/BATCHWISE";
                ClearControls();
                AddToolTip();
                HidepnlGO();

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


        #endregion

        # region IReport Members

        public override void Export(string ExportFileName)
        {
            base.Export(ExportFileName);

            string filename = "Report";
            if (ExportFileName != null && ExportFileName.ToString() != string.Empty)
                filename = ExportFileName.ToString().Trim();
            string filePath = "d:\\reports\\" + filename + ".csv";
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            PrintReportHead = "Schedule H1 Sale List From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
            PrintReportHead2 = "[" + txtViewtext.Text.ToString() + "]";
            try
            {
                GeneralReport.ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                GeneralReport.ExportPrintDetails(dgvReportList, filePath);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override void EMail(string ExportFileName, string EmailID)
        {
            base.EMail(ExportFileName, EmailID);
            string filename = "Report";
            string tosendemailID = "sheelabsharma@gmail.com";
            if (ExportFileName != null && ExportFileName.ToString() != string.Empty)
                filename = ExportFileName.ToString().Trim();
            if (EmailID != null && EmailID.ToString() != string.Empty)
                tosendemailID = EmailID.ToString().Trim();
            string filePath = "d:\\reports\\" + filename + ".csv";
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            try
            {
                GeneralReport.ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                GeneralReport.EmailPrintDetails(dgvReportList, filePath, tosendemailID);
                // Sir here I am sending grid,filepath for all reports

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                PrintReportHead = "Schedule H1 Sales Report From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;               
                // totalrows += 7; // for first page heading
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_ID"].Value != null)
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
                        



                        if (dr.Cells["Col_Srno"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Srno"].Value.ToString().PadLeft(5), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Name"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Name"].Value.ToString().PadRight(30).Substring(0,25), PrintRowPixel, 35, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Doctor"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Doctor"].Value.ToString().PadRight(30).Substring(0,25), PrintRowPixel, 200, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherType"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 360, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VoucherNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 400, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherDate"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 440, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Product"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Product"].Value.ToString().PadRight(30).Substring(0,25), PrintRowPixel, 500, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                       
                        if (dr.Cells["Col_Quantity"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 720, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                       
                       
                    }
                }

                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        //public int CalculateTotalRows(int totrows)
        //{
        //    foreach (DataGridViewRow dr in dgvReportList.Rows)
        //    {

        //        if (dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Batch"].Value.ToString() == "Total")
        //            totrows += 2;
        //    }
        //    return totrows;
        //}

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

                row = new PrintRow("SrNo", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Patient", PrintRowPixel, 50, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Doctor", PrintRowPixel, 210, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Vou Type Number  Date", PrintRowPixel, 350, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Product", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 725, PrintFont);
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
        public void ClearControls()
        {
            try
            {

                lblFooterMessage.Text = "";

                InitializeReportGrid();
                fromDate1.Value = DateTime.Now;
                toDate1.Value = DateTime.Now;
                DataTable dtable = new DataTable();
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
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SrNo";
                column.HeaderText = "Srno";
                column.Width = 50;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.HeaderText = "Party";
                column.Width = 200;
                dgvReportList.Columns.Add(column);



                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Doctor";
                column.HeaderText = "Doctor";
                column.Width = 200;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.HeaderText = "";
                column.Width = 25;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "DATE";
                column.DefaultCellStyle.Format = "d";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Product";
                column.HeaderText = "Product";
                column.Width = 200;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 50;
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

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
        }
        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
        }

        private void FillAccountData(string ProductID, string batch, double mrp)
        {
            int _RowIndex;
            DataGridViewRow currentdr = null;
            try
            {

                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                currentdr.Cells["Col_ID"].Value = ProductID;

                int msrno = 0;
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    string drproudtid = "";
                    string drbatch = "";
                    double drmrp = 0;

                    drproudtid = dr["ProductID"].ToString();
                    drbatch = dr["BatchNumber"].ToString();
                    drmrp = Convert.ToDouble(dr["MRP"].ToString());

                    _RowIndex = dgvReportList.Rows.Add();
                    currentdr = dgvReportList.Rows[_RowIndex];
                    msrno += 1;

                    currentdr.Cells["Col_ID"].Value = dr["MasterSaleID"].ToString();
                    currentdr.Cells["Col_Srno"].Value = msrno.ToString("#0");
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    currentdr.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                    currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                    currentdr.Cells["Col_Name"].Value = dr["PatientName"].ToString();
                    currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
                    currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();


                }
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_ID"].Value = ProductID;


            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }

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
                    lblFooterMessage.Text = "";
                    ShowpnlGO();
                    InitializeReportGrid();
                    if (dgvReportList.Rows.Count > 0)
                        dgvReportList.Rows.Clear();
                    FillReportData();
                    BindReportGrid();
                    this.Cursor = Cursors.Default;
                    NoofRows();
                    dgvReportList.Focus();
                }
                else
                    lblFooterMessage.Text = "Please Check Date...";


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
                dtable = _SaleList.GetSaleDataForH1(_MFromDate, _MToDate);
                _BindingSource = dtable;

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






        private void BindReportGrid()
        {
            try
            {
                int _RowIndex;
                int _Msrno = 0;
                string _MProduct = "";
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;


                foreach (DataRow dr in _BindingSource.Rows)
                {




                    _RowIndex = dgvReportList.Rows.Add();
                    currentdr = dgvReportList.Rows[_RowIndex];
                    _Msrno += 1;
                    _MProduct = "";
                    _MProduct = dr["ProdName"].ToString() + " " + dr["ProdLoosePack"].ToString() + " " + dr["ProdPack"].ToString();
                    currentdr.Cells["Col_ID"].Value = dr["MasterSaleID"].ToString();
                    currentdr.Cells["Col_Srno"].Value = _Msrno.ToString("#0");
                    currentdr.Cells["Col_Name"].Value = dr["PatientShortName"].ToString();
                    currentdr.Cells["Col_Doctor"].Value = dr["DoctorShortName"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                    currentdr.Cells["Col_Product"].Value = _MProduct;
                    currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
                }
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }



        #endregion

        # region Events



        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            btnOKMultiSelectionClick();
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
                    if (vousubtype == FixAccounts.SubTypeForPatientSale)
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


        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                //ttSaleProductBatch.SetToolTip(mcbProduct, "Select Product and Press Enter");
                //ttSaleProductBatch.SetToolTip(cbSelectAll, "Check to Select All Products in the List and Click OK");
                //ttSaleProductBatch.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                //ttSaleProductBatch.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion
    }
}
