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

namespace PharmaSYSRetailPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclMISListStockStatementBank : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        #endregion

        # region Constructor
        public UclMISListStockStatementBank()
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
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "MIS - STOCK STATEMENT BANK";
                ClearControls();
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

        public override void Print()
        {
            try
            {
                string reportHeading = General.GetReportHeading();
                reportHeading += "Stock Statement (Bank) From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                PrintDataGrid.PrintDGV.Print_DataGridView(dgvReportList.GridView, reportHeading, true, true);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 80;
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
                column.Width = 126;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 130;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Rate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 150;
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
                string startdate = General.GetDateInDateFormat(General.ShopDetail.Shopsy);
                FromDate.Text = startdate;
                string enddate = General.GetDateInDateFormat(General.ShopDetail.Shopey);
                ToDate.Text = enddate;
                FromDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                FromDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                ToDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                ToDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                lblFooterMessage.Text = "";

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
                dgvReportList.DateColumnNames.Add("Col_VoucherDate");
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
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
                DataTable dtable = new DataTable();
                dtable = _SaleList.GetOverviewData();
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }     
        #endregion

        # region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                FillReportGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion
    }
}
