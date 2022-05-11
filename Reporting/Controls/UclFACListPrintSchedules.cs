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

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclFACListPrintSchedules : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        #endregion

        # region Constructor
        public UclFACListPrintSchedules()
        {
            InitializeComponent();
        }
        #endregion

        #region IOverview Members
        public override void ShowOverview()
        {
            _BindingSource = new DataTable();
            _SaleList = new SaleList();
            _MFromDate = General.ShopDetail.Shopsy;
            _MToDate = General.ShopDetail.Shopey;
            headerLabel1.Text = "FINAL ACCOUNTS - PRINT SCHEDULES";
            ConstructReportColumns();        
            FillReportGrid();
            dgvReportList.Focus();
        }
        #endregion

        # region IReport Members
     
        public override void Print()
        {
            try
            {
                string reportHeading = General.GetReportHeading();
                reportHeading += "Schedules From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                PrintDataGrid.PrintDGV.Print_DataGridView(dgvReportList.GridView, reportHeading, true, true);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }      
        #endregion

        # region Other Private methods
        private void ConstructReportColumns()
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

        private void FillReportGrid()
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
    
        private void FillReportData()
        {
            DataTable dtable = new DataTable();
            dtable = _SaleList.GetOverviewData();
            _BindingSource = dtable;
        }      
        #endregion

        # region Events
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            FillReportGrid();
        }
        #endregion

    }
}
