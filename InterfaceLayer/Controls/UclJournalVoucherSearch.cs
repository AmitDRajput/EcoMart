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
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclJournalVoucherSearch : SearchControl
    {
        # region declaration
        private DataTable _BindingSource;
        private JournalVoucher _JournalVoucher;
        #endregion

        #region Constructor
        public UclJournalVoucherSearch()
        {
            try
            {
                InitializeComponent();
                this.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region IOverview
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _JournalVoucher = new JournalVoucher();
                headerLabelForOverView1.Text = "JOURNAL VOUCHER -> SEARCH";
                ConstructSearchColumns();
               
                dgvSearchList.ShowGridFilter = true;
                InitializeDate();
                FillSearchGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void SetFocus()
        {
            dgvSearchList.SetFocus();
        }
        public override string SelectedID()
        {
            string retValue = "";
            try
            {
                if (dgvSearchList.Rows.Count > 0)
                {
                    retValue = dgvSearchList.SelectedRow.Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        #endregion IOverview

        #region Other private methods

        private void ConstructSearchColumns()
        {
            dgvSearchList.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SerialNumber";
                column.DataPropertyName = "SerialNumber";
                column.HeaderText = "SrNo.";
                //column.Visible = false;
                column.Width = 100;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Account";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address1";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNo";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Debit";
                column.DataPropertyName = "Debit";
                column.HeaderText = "Debit";
                column.Width = 100;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Credit";
                column.DataPropertyName = "Credit";
                column.HeaderText = "Credit";
                column.Width = 180;
                dgvSearchList.Columns.Add(column);
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        public override void ProcessDateFromToClick()
        {
            FillSearchGrid();
        }
        private void FillSearchGrid()
        {
            try
            {
                FillSearchGridData();
                dgvSearchList.DataSource = _BindingSource;
                dgvSearchList.Bind();
                dgvSearchList.DateColumnNames.Add("Col_VoucherDate");
                //dgvSearchList.DoubleColumnNames.Add("Col_Amount");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillSearchGridData()
        {
            DataTable dtable = new DataTable();
            try
            {
                dtable = _JournalVoucher.GetOverviewJVData(_JournalVoucher.JVVouType, General.ConvertDateToISODateString(dtFrom.Value), General.ConvertDateToISODateString(dtTo.Value));
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region Events
        private void dgvSearchList_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvSearchList.SelectedRow != null)
                {
                    GridRowDoubleClicked();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

    }
}
