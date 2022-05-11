using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using EcoMart.InterfaceLayer;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclFACListProfitandLoss : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private FinalAccounts fa;
        //private Account _Account;
        private string _MFromDate;
        private string _MToDate;
        private double _ClosingStock = 0;
        private DataTable _Tradingleft;
        private DataTable _Tradingright;
        private DataTable _PnLLeft;
        private DataTable _PnlRight;
        #endregion

        # region Constructor
        public UclFACListProfitandLoss()
        {
            InitializeComponent();
        }
        #endregion

        #region IOverview Members
        public override void ShowOverview()
        {
            _BindingSource = new DataTable();
            _SaleList = new SaleList();
            fa = new FinalAccounts();
            // _Account = new Account();
            _MFromDate = string.Empty;
            _MToDate = string.Empty;
            DataRow dr = fa.GetTrialBalanceDates();
            if (dr == null)
            {
                System.Windows.MessageBox.Show("Accounting Year Not Found");
                btnOKMultiSelection1.Enabled = false;
            }
            else
            {
                if (dr != null)
                {
                    if (dr["setTrialBalanceDateFrom"] != DBNull.Value)
                        _MFromDate = dr["setTrialBalanceDateFrom"].ToString();
                    if (dr["setTrialBalanceDateTo"] != DBNull.Value)
                        _MToDate = dr["setTrialBalanceDateTo"].ToString();
                    if (_MFromDate == string.Empty)
                    {
                        System.Windows.MessageBox.Show("Please Do Trial Balance First");
                        btnOKMultiSelection1.Enabled = false;
                    }
                    else
                    {
                        btnOKMultiSelection1.Enabled = true;
                        fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
                        toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
                        txtClosingStock.Focus();
                    }
                }
            }
            headerLabel1.Text = "FINAL ACCOUNTS - PROFIT AND LOSS ";

        }
        #endregion

        # region IReport Members


        public override void Print()
        {
            try
            {
                string reportHeading = General.GetReportHeading();
                reportHeading += "Profit and Loss Account From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                PrintDataGrid.PrintDGV.Print_DataGridView(dgvTradingLeft.GridView, reportHeading, true, true);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Other Private methods
        private void ConstructdgvTradingLeftColumns()
        {
            dgvTradingLeft.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvTradingLeft.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountName";
            column.HeaderText = "Particulars";
            column.Width = 200;
            dgvTradingLeft.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.HeaderText = "Amount";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 150;
            dgvTradingLeft.Columns.Add(column);
        }
        private void ConstructdgvTradingRightColumns()
        {
            dgvTradingRight.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvTradingRight.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountName";
            column.HeaderText = "Particulars";
            column.Width = 200;
            dgvTradingRight.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.HeaderText = "Amount";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 150;
            dgvTradingRight.Columns.Add(column);
        }

        private void ConstructdgvProfitAndLossLeftColumns()
        {
            dgvProfitAndLossLeft.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvProfitAndLossLeft.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountName";
            column.HeaderText = "Particulars";
            column.Width = 200;
            dgvProfitAndLossLeft.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.HeaderText = "Amount";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 150;
            dgvProfitAndLossLeft.Columns.Add(column);
        }
        private void ConstructdgvProfitAndLossRightColumns()
        {
            dgvProfitAndLossRight.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.Visible = false;
            dgvProfitAndLossRight.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_AccountName";
            column.HeaderText = "Particulars";
            column.Width = 200;
            dgvProfitAndLossRight.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.HeaderText = "Amount";
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 150;
            dgvProfitAndLossRight.Columns.Add(column);
        }
        private void FillReportGrid()
        {
            FillReportData();
            dgvTradingLeft.DataSource = _BindingSource;
            dgvTradingLeft.DoubleColumnNames.Add("Col_Amount1");
            dgvTradingLeft.Bind();
            int noofrecords = dgvTradingLeft.Rows.Count;
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
            ButtonOKClick();
        }

        private void ButtonOKClick()
        {
            bool retValue = false;
            pnlMultiSelection1.Visible = false;
            if (txtClosingStock.Text != null && txtClosingStock.Text.ToString() != string.Empty)
                _ClosingStock = Convert.ToDouble(txtClosingStock.Text.ToString());
            retValue = fa.ClearProfitAndLossfrommasterGroup(_ClosingStock);
            double _Profit = fa.GetProfit();
            if (_Profit < 0)
            {
                retValue = fa.UpdateLossAccounts(Math.Abs(_Profit));
            }
            else
            {
                retValue = fa.UpdateProfitAccounts(_Profit);
            }

            ConstructdgvProfitAndLossLeftColumns();
            ConstructdgvProfitAndLossRightColumns();
            ConstructdgvTradingLeftColumns();
            ConstructdgvTradingRightColumns();
            _Tradingleft = fa.GetTradingDebitRows();
            _Tradingright = fa.GetTradingCreditRows();
            FillTradingLeft(_Tradingleft);
            FillTradingRight(_Tradingright);

            _Profit = fa.GetProfitForPnl();            
            if (_Profit < 0)
            {
                retValue = fa.UpdateLossAccountsForPnL(Math.Abs(_Profit));
            }
            else
            {
                retValue = fa.UpdateProfitAccountsForPnL(_Profit);
            }
            _PnLLeft = fa.GetProfitAndLossLeftRows();
            _PnlRight = fa.GetProfitAndLossRightRows();
            FillPnLLeft(_PnLLeft);
            FillPnLRight(_PnlRight);


        }

        private void FillPnLRight(DataTable _PnlRight)
        {
            string mgroupid = "";
            string mgroupname = "";
            double mamt = 0;
            int rowindex = 0;
            foreach (DataRow dr in _PnlRight.Rows)
            {
                mgroupid = "";
                mgroupname = "";
                mamt = 0;
                if (dr["GroupID"] != DBNull.Value)
                {
                    mgroupid = dr["GroupID"].ToString();
                }

                if (dr["GroupName"] != DBNull.Value)
                {
                    mgroupname = dr["GroupName"].ToString();
                }

                if (dr["db"] != DBNull.Value)
                {
                    mamt = Convert.ToDouble(dr["db"].ToString());
                }
                rowindex = dgvProfitAndLossRight.Rows.Add();
                dgvProfitAndLossRight.Rows[rowindex].Cells["Col_ID"].Value = mgroupid;
                dgvProfitAndLossRight.Rows[rowindex].Cells["Col_AccountName"].Value = mgroupname;
                dgvProfitAndLossRight.Rows[rowindex].Cells["Col_Amount"].Value = mamt.ToString("#0.00");
            } 
        }

        private void FillPnLLeft(DataTable _PnLLeft)
        {
            string mgroupid = "";
            string mgroupname = "";
            double mamt = 0;
            int rowindex = 0;
            foreach (DataRow dr in _PnLLeft.Rows)
            {
                mgroupid = "";
                mgroupname = "";
                mamt = 0;
                if (dr["GroupID"] != DBNull.Value)
                {
                    mgroupid = dr["GroupID"].ToString();
                }

                if (dr["GroupName"] != DBNull.Value)
                {
                    mgroupname = dr["GroupName"].ToString();
                }

                if (dr["db"] != DBNull.Value)
                {
                    mamt = Convert.ToDouble(dr["db"].ToString());
                }
                rowindex = dgvProfitAndLossLeft.Rows.Add();
                dgvProfitAndLossLeft.Rows[rowindex].Cells["Col_ID"].Value = mgroupid;
                dgvProfitAndLossLeft.Rows[rowindex].Cells["Col_AccountName"].Value = mgroupname;
                dgvProfitAndLossLeft.Rows[rowindex].Cells["Col_Amount"].Value = mamt.ToString("#0.00");
            } 
        }

        private void FillTradingRight(DataTable _Tradingright)
        {
            string mgroupid = "";
            string mgroupname = "";
            double mamt = 0;
            int rowindex = 0;
            foreach (DataRow dr in _Tradingright.Rows)
            {
                mgroupid = "";
                mgroupname = "";
                mamt = 0;
                if (dr["GroupID"] != DBNull.Value)
                {
                    mgroupid = dr["GroupID"].ToString();
                }

                if (dr["GroupName"] != DBNull.Value)
                {
                    mgroupname = dr["GroupName"].ToString();
                }

                if (dr["db"] != DBNull.Value)
                {
                    mamt = Convert.ToDouble(dr["db"].ToString());
                }
                rowindex = dgvTradingRight.Rows.Add();
                dgvTradingRight.Rows[rowindex].Cells["Col_ID"].Value = mgroupid;
                dgvTradingRight.Rows[rowindex].Cells["Col_AccountName"].Value = mgroupname;
                dgvTradingRight.Rows[rowindex].Cells["Col_Amount"].Value = mamt.ToString("#0.00");
            }
        }

        private void FillTradingLeft(DataTable _Tradingleft)
        {
            string mgroupid = "";
            string mgroupname = "";
            double mamt = 0;
            int rowindex = 0;
            foreach (DataRow dr in _Tradingleft.Rows)
            {
                mgroupid = "";
                mgroupname = "";
                mamt = 0;
                if (dr["GroupID"] != DBNull.Value)
                {
                    mgroupid = dr["GroupID"].ToString();
                }

                if (dr["GroupName"] != DBNull.Value)
                {
                    mgroupname = dr["GroupName"].ToString();
                }

                if (dr["db"] != DBNull.Value)
                {
                    mamt = Convert.ToDouble(dr["db"].ToString());
                }
                rowindex = dgvTradingLeft.Rows.Add();
                dgvTradingLeft.Rows[rowindex].Cells["Col_ID"].Value = mgroupid;
                dgvTradingLeft.Rows[rowindex].Cells["Col_AccountName"].Value = mgroupname;
                dgvTradingLeft.Rows[rowindex].Cells["Col_Amount"].Value = mamt.ToString("#0.00");
            }
        }
        #endregion

        private void txtClosingStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _ClosingStock = Convert.ToDouble(txtClosingStock.Text.ToString());
                ButtonOKClick();
            }
        }

    }
}
