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
    public partial class UclStockListOpeningStock : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private OPStock _OPStock;
        private string _MFromDate;
        private string _MToDate;
        #endregion

        #region Constructor
        public UclStockListOpeningStock()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclOPStock();
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
                _BindingSource = new DataTable();
                _OPStock = new OPStock();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "STOCK - OPENING STOCK REGISTER";
                ConstructReportColumns();
                ClearControls();
                FillReportData();
                pnlMultiSelection1.Visible = true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void SetFocus()
        {
            fromDate1.Focus();
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
          //      btnOKMultiSelectionClick();
                retValue = true;
            }

            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }
        public override void Print()
        {
            try
            {
                string reportHeading = General.GetReportHeading();
                reportHeading += "Opening Stock List: " ;
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
                string startdate = General.GetDateInDateFormat(General.ShopDetail.Shopsy);
                fromDate1.Text = startdate;
                string enddate = General.GetDateInDateFormat(General.ShopDetail.Shopey);
                toDate1.Text = enddate;
                fromDate1.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                fromDate1.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                toDate1.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                toDate1.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                lblFooterMessage.Text = "";
                txtReportTotalAmount.Text = "";
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
                column.DataPropertyName = "MasterID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";
                column.Width = 200;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 400;
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
                dgvReportList.DataSource = _BindingSource;            
                dgvReportList.DateColumnNames.Add("Col_VoucherDate");
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
                dgvReportList.Bind();
                CheckFilter();
                double totamt = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    totamt += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                }
                txtReportTotalAmount.Text = totamt.ToString("#0.00");
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void CheckFilter()
        {
           
            try
            {

                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
            
                _BindingSource.DefaultView.RowFilter = "Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" +  _MToDate + "'";
            }
            catch (Exception Ex) 
            {
                Log.WriteError("UclStockListOpeningStock.CheckFiler>> " + Ex.Message);
                
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
                dtable = _OPStock.GetOverviewData();
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
                pnlMultiSelection1.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                FillReportGrid();
                this.Cursor = Cursors.Default;
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
        #endregion

        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }
}
