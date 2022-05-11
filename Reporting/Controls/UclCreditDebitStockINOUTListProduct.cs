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
    public partial class UclCreditDebitStockINOUTListProduct : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        public DataTable selectedcompanies;
        private CreditDebitNote _CreditDebitNote;
        private string _MFromDate;
        private string _MToDate;
        # endregion

        public UclCreditDebitStockINOUTListProduct()
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
        # region IOverview
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _CreditDebitNote = new CreditDebitNote();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                ClearControls();
                headerLabel1.Text = "STOCK IN/OUT LIST (PRODUCT)";
                ConstructReportColumns();
                AddToolTip();
                FillProductCombo();
                FillReportGrid();
                mcbProduct.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
                string reportHeading = General.GetReportHeading();                      
                PrintDataGrid.PrintDGV.Print_DataGridView(dgvReportList.GridView, reportHeading, true, true);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }      
     
        # endregion

        #region Construct Fill
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
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 75;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 85;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "AccountID";
                column.Visible = false;
                column.Width = 115;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 190;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 190;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CVouTypeArea";
                column.DataPropertyName = "ClearedInVoucherType";
                column.HeaderText = "BillType";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CVoucherNumber";
                column.DataPropertyName = "ClearedInVoucherNumber";
                column.HeaderText = "Bill.NO";
                column.Width = 75;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CVoucherDate";
                column.DataPropertyName = "ClearedInVoucherDate";
                column.HeaderText = "Bill.Date";
                column.Width = 85;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 125;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public void ClearControls()
        {
            try
            {
               // txtAmount.Text = "0.00";
                InitializeReportGrid();
                InitializeDates();
                lblFooterMessage.Text = "";
               // txtReportTotalAmount.Text = "";
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
        private void FillReportGrid()
        {
            try
            {
                FillReportData();
                dgvReportList.DataSource = _BindingSource;             
                dgvReportList.DateColumnNames.Add("Col_VoucherDate");
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
                dgvReportList.DateColumnNames.Add("Col_CVoucherDate");
                dgvReportList.Bind();
                int noofrecords = dgvReportList.Rows.Count;
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
                DataTable dtable = new DataTable();
                _BindingSource = dtable;
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
                //    DataTable dtable = General.ProductList;
                Product prod = new Product();
                DataTable dtable = prod.GetOverviewData();
                mcbProduct.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        # endregion
      
        # region events
        private void btnOK_Click(object sender, EventArgs e)
        {
            btnOKClick();
        }

        private void btnOKClick()
        {
            try
            {
                FillReportGrid();
                PrintReportHead = "Stock In/Out From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                PrintReportHead2 = "";     
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                btnOKClick();
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                fromDate1.Focus();
        }
        # endregion Events

        #region AddToolTip
        private void AddToolTip()
        {
            try
            {
                //ttToolTip.SetToolTip(btnOKMultiSelection, "Click to See Report = End");
                //ttToolTip.SetToolTip(pnlMultiSelection, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion AddToolTip
    }
}
