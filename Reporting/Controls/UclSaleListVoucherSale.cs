using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSDistributorPlus.InterfaceLayer;
using PharmaSYSDistributorPlus.Printing;

namespace PharmaSYSDistributorPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSaleListVoucherSale : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private VoucherSaleList _VoucherSaleList;
        private string _MFromDate;
        private string _MToDate;
        #endregion

        # region Constructor
        public UclSaleListVoucherSale()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclPatientSale(); 
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
                _VoucherSaleList = new VoucherSaleList();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "DAILY VOUCHER SALE";
                ConstructReportColumns();
                ClearControls();
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
                string reportHeading = General.GetReportHeading();
                reportHeading += "Sale VoucherSale From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                PrintDataGrid.PrintDGV.Print_DataGridView(dgvReportList.GridView, reportHeading, true, true);

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
                string startdate = DateTime.Now.Date.ToString();
                FromDate.Text = startdate;
                string enddate = DateTime.Now.Date.ToString();
                ToDate.Text = enddate;
                FromDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                FromDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                ToDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                ToDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                lblFooterMessage.Text = "";
                txtReportTotalAmount.Text = "";
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                dgvReportList.DataSource = _BindingSource;              
                dgvReportList.DateColumnNames.Add("Col_VoucherDate");
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
                _BindingSource.DefaultView.Sort = "VoucherDate";
                dgvReportList.Bind();
                double totamt = 0;

                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    totamt += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                }

                txtReportTotalAmount.Text = totamt.ToString("#0.00");

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
                string mfromdate = "";
                string mtodate = "";

                mfromdate = FromDate.Value.Date.ToString("yyyyMMdd");
                mtodate = mfromdate;
                //    mtodate = ToDate.Value.Date.ToString("yyyyMMdd");
                dtable = _VoucherSaleList.GetOverviewDataForVoucherSaleReport(mfromdate);
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
                PrintReportHead = "Sale VoucherSale From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                PrintReportHead2 = "";
                FillReportGrid();
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
                    string voutype = dgvReportList.SelectedRow.Cells["Col_Type"].Value.ToString();
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
     

    }
}
