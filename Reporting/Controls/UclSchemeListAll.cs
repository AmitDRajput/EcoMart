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
    public partial class UclSchemeListAll : ReportBaseControl
    {

        #region Declaration
        private DataTable _BindingSource;
        private Scheme _Scheme;
        private string _MFromDate;
        private string _MToDate;
        #endregion

        # region Constructor

        public UclSchemeListAll()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclScheme();
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
                _Scheme = new Scheme();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "SCHEME LIST (ALL)";
                PrintReportHead = "Scheme List [All]";
                PrintReportHead2 = "";
                ConstructReportColumns();               
                FillReportGrid();
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }       
       
        #endregion

        # region IReport Members

        public DataTable GetReportData()
        {
            return _BindingSource.Copy();
        }
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
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

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

                        int length = Math.Min(dr.Cells["Col_ProductName"].Value.ToString().Length, 20);
                        row = new PrintRow((dr.Cells["Col_ProductName"].Value.ToString()).Substring(0, length), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_ProductLoosePack"].Value.ToString(), PrintRowPixel, 200, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_ProductPack"].Value.ToString(), PrintRowPixel, 240, PrintFont);
                        PrintBill.Rows.Add(row);
                        int minteger = Convert.ToInt32(dr.Cells["Col_Quantity1"].Value.ToString());
                        if (minteger > 0)
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 320, PrintFont);
                        PrintBill.Rows.Add(row);
                        minteger = Convert.ToInt32(dr.Cells["Col_Scheme1"].Value.ToString());
                        if (minteger > 0)
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 370, PrintFont);
                        PrintBill.Rows.Add(row);
                        minteger = Convert.ToInt32(dr.Cells["Col_Quantity2"].Value.ToString());
                        if (minteger > 0)
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 420, PrintFont);
                        PrintBill.Rows.Add(row);
                        minteger = Convert.ToInt32(dr.Cells["Col_Scheme2"].Value.ToString());
                        if (minteger > 0)
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 470, PrintFont);
                        PrintBill.Rows.Add(row);
                        minteger = Convert.ToInt32(dr.Cells["Col_Quantity3"].Value.ToString());
                        if (minteger > 0)
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 520, PrintFont);
                        PrintBill.Rows.Add(row);
                        minteger = Convert.ToInt32(dr.Cells["Col_Scheme3"].Value.ToString());
                        if (minteger > 0)
                            row = new PrintRow(minteger.ToString("#0"), PrintRowPixel, 570, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_StartDate"].Value.ToString()), PrintRowPixel, 620, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_EndDate"].Value.ToString()), PrintRowPixel, 700, PrintFont);
                        PrintBill.Rows.Add(row);
                       
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
            PrintRowCount = 0;
            return PrintRowCount;
        }


        #endregion

        # region Other Private methods
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
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 210;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
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
                column.Name = "Col_EndDate";
                column.DataPropertyName = "ClosingDate";
                column.HeaderText = "EndDate";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                HideColumns();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void HideColumns()
        {
            try
            {
                dgvReportList.Columns[0].Visible = false;
                dgvReportList.Columns[1].Visible = false;
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
                dgvReportList.DateColumnNames.Add("Col_StartDate");
                dgvReportList.DateColumnNames.Add("Col_EndDate");
                dgvReportList.Bind();
                int noofrecords = dgvReportList.Rows.Count;
                if (noofrecords == 0)
                    lblFooterMessage.Text = "NO Records ";
                else if (noofrecords == 1)
                    lblFooterMessage.Text = "Record : " + noofrecords.ToString().Trim();
                else
                    lblFooterMessage.Text = "Records : " + noofrecords.ToString().Trim();
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
                ConstructReportColumns();
                DataTable dtable = new DataTable();
                dtable = _Scheme.GetOverviewDataForSchemeListAll();
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion


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

       
    }
}
