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

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclCounterSaleSearch : SearchControl
    {
        # region declaration
        private DataTable _BindingSource;
        private SSSale _CounterSale;
        #endregion

        # region constructor

        public UclCounterSaleSearch()
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

        #region IOverview Members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _CounterSale = new SSSale();
                headerLabelForOverView1.Text = "COUNTERSALE -> SEARCH";
                ConstructSearchColumns();
                FillSearchGrid();
                dgvSearchList.ShowGridFilter = true;
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
                column.DataPropertyName = "Id";
                column.HeaderText = "ID";
                column.Width = 80;
                column.Visible = false;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VouType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Sale Type";
                column.Width = 60;
                column.Visible = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VouNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;
                column.Visible = true;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 100;
                column.Visible = true;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientName";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Patient Name";
                column.Width = 150;
                column.Visible = true;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientAddress";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 110;
                column.Visible = true;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);


                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_DocName";
                //column.DataPropertyName = "DoctorName";
                //column.HeaderText = "Doctor";
                //column.Width = 200;
                //column.Visible = true;
                ////column.ReadOnly = true;
                //dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 100;
                column.Visible = true;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
      
        private void FillSearchGrid()
        {
            try
            {
                FillSearchGridData();
                dgvSearchList.DataSource = _BindingSource;
                dgvSearchList.Bind();
                dgvSearchList.DateColumnNames.Add("Col_VoucherDate");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillSearchGridData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _CounterSale.GetOverviewData(FixAccounts.SubTypeForVoucherSale);
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
