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
    public partial class UclCashExpenseSalaryList : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;      
        private CashExpenseSalary _CashExpenseSalary;
        private string _MFromDate;
        private string _MToDate;
        # endregion

        #region Constructor
        public UclCashExpenseSalaryList()
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

      

        # region IOverview
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _CashExpenseSalary = new CashExpenseSalary();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "VOUCHER LIST-CASH EXPENSE SALARY";
                ConstructReportColumns();
                ClearControls();
                FillPartyCombo();
                FillReportData();
                FillReportGrid();
                mcbParty.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void Print()
        {
            try
            {
                string reportHeading = General.GetReportHeading();
                reportHeading += "Cash Expenses Salary Book From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
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
              // _VouType = FixAccounts.VoucherTypeForCashExpensesSalary;
                txtAmount.Text = "0.00";
                mcbParty.SelectedID = "";
                string startdate = General.GetDateInDateFormat(General.ShopDetail.Shopsy);
                FromDate.Text = startdate;
                string enddate = General.GetDateInDateFormat(General.ShopDetail.Shopey);
                ToDate.Text = enddate;
                FromDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                FromDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                ToDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                ToDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                lblFooterMessage.Text = "";
                ConstructReportColumns();
                _BindingSource = new DataTable();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
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
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
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
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Area";
                column.DataPropertyName = "AreaName";
                column.HeaderText = "Area";
                column.Width = 240;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 140;
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
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }          
        }
        private void CheckFilter()
        {
            double mamount = 0;
            string partycode = "";        
            try
            {
                _MFromDate = FromDate.Value.Date.ToString("yyyyMMdd");
                _MToDate = ToDate.Value.Date.ToString("yyyyMMdd");
                if (txtAmount.Text != null && txtAmount.Text.ToString() != "")
                    double.TryParse(txtAmount.Text.ToString(), out mamount);
                if (mcbParty.SelectedID != null && mcbParty.SelectedID != "")
                    partycode = mcbParty.SelectedID;
               

                if (mamount > 0)
                {
                    if (partycode == "")
                        _BindingSource.DefaultView.RowFilter = "AmountNet = " + mamount + " and Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" +  _MToDate + "'";
                    else
                        _BindingSource.DefaultView.RowFilter = "AmountNet = " + mamount + " AND  AccountID = '" + partycode + "' and voucherdate <= '" +  _MToDate + "'";
                }
                else
                {
                    if (partycode != "")
                        _BindingSource.DefaultView.RowFilter = "AccountID = '" + partycode + "' and Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" +  _MToDate + "'";
                    else
                        _BindingSource.DefaultView.RowFilter = "Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" +  _MToDate + "'";
                }
                NoofRows();
            }
            catch (Exception ex) { Log.WriteException(ex); }

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
                dtable = _CashExpenseSalary.GetOverviewData(VouType);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillPartyCombo()
        {
            try
            {
                mcbParty.SelectedID = null;
                mcbParty.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAreaID" };
                mcbParty.ColumnWidth = new string[5] { "0", "20", "200", "200", "0" };
                mcbParty.DisplayColumnNo = 2;
                mcbParty.ValueColumnNo = 0;
                mcbParty.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetOverviewDataForList();
                mcbParty.FillData(dtable);
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
            try
            {
                FillReportGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        # endregion
    }
}
