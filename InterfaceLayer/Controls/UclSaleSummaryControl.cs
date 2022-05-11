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

namespace EcoMart.InterfaceLayer
{
    public partial class UclSaleSummaryControl : UserControl
    {
        public UclSaleSummaryControl()
        {
            InitializeComponent();
            ConstructReportColumns();
        }

        #region Summary

        #region Declare
        private string _MFromDate;
        private string _MToDate;
        private SaleList _SaleList;
        private DataTable _BindingSource;
        #endregion Declare

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.Visible = true;
                column.HeaderText = "Vou Type";
                column.Width = 90;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Description";
                column.HeaderText = "Description";
                column.Width = 300;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.HeaderText = "ProfitInRupees";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public void btnOKMultiSelectionClick()
        {
            dgvReportList.Rows.Clear();
            _SaleList = new SaleList();
            bool retValue = false;
            _MFromDate = DateTime.Now.Date.ToString("yyyyMMdd");
            _MToDate = DateTime.Now.Date.ToString("yyyyMMdd");
            retValue = General.CheckDates(_MFromDate, _MToDate);
            if (retValue)
            {
                ShowpnlGO();
                FillReportGridData();
            }
            if(General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y")
            {
                dgvReportList.Columns["Col_ProfitInRupees"].Visible = true;
            }
            else
                dgvReportList.Columns["Col_ProfitInRupees"].Visible = false;
        }
        public void ShowpnlGO()
        {
            //pnlMultiSelection1.Visible = false;            
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
        }
        private void FillReportGridData()
        {
            try
            {
                FillReportData();
                FormatGrid();
                BindGrid();
                int noofrecords = dgvReportList.Rows.Count;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void BindGrid()
        {
            double _TotalSale = 0;
            try
            {
                bool RowAdded;
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    RowAdded = false;
                    int rowindex = dgvReportList.Rows.Add();
                    DataGridViewRow gdr = dgvReportList.Rows[rowindex];
                    double amount = 0;
                    amount = Convert.ToDouble(dr["AmountNet"].ToString());

                    if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCashSale)
                    {
                        gdr.Cells["Col_Description"].Value = "Cash Sale";
                        _TotalSale += amount;
                        RowAdded = true;
                    }
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCreditSale)
                    {
                        gdr.Cells["Col_Description"].Value = "Credit Sale";
                        _TotalSale += amount;
                        RowAdded = true;
                    }
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCreditStatementSale)
                    {
                        gdr.Cells["Col_Description"].Value = "Credit Statement Sale";
                        _TotalSale += amount;
                        RowAdded = true;
                    }

                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        gdr.Cells["Col_Description"].Value = "Voucher Sale";
                        _TotalSale += amount;
                        RowAdded = true;
                    }     
                    if (RowAdded == true)
                    {
                        gdr.Cells["Col_ID"].Value = dr["VoucherType"].ToString();
                        gdr.Cells["Col_ProfitInRupees"].Value = Convert.ToDouble(dr["ProfitInRupees"]).ToString("#0.00");
                        gdr.Cells["Col_Amount"].Value = amount.ToString("#0.00");
                    }
                    else
                        dgvReportList.Rows.RemoveAt(gdr.Index);
                }

                int index = dgvReportList.Rows.Add();
                DataGridViewRow row = dgvReportList.Rows[index];
                row.Cells["Col_Description"].Value = "Total Sale";
                row.Cells["Col_Amount"].Value = _TotalSale.ToString("#0.00");
                row.DefaultCellStyle.BackColor = Color.MistyRose;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatGrid()
        {
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DoubleColumnNames.Add("Col_ProfitInRupees");
        }

        private void FillReportData()
        {
            try
            {
                _MFromDate = DateTime.Now.Date.ToString("yyyyMMdd");
                _MToDate = DateTime.Now.Date.ToString("yyyyMMdd");
                bool retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    DataTable dtable = new DataTable();
                    dtable = _SaleList.GetDataForSaleSummary(_MFromDate, _MToDate);
                    _BindingSource = dtable;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion Summary
    }
}
