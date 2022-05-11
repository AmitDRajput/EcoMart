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
    public partial class UclAccountSearch : SearchControl
    {
        #region Declaration
        private DataTable _BindingSource;      
        private Account _Account;    
        #endregion

        #region Constructor
        public UclAccountSearch()
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

        #region ShowOverview
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Account = new Account();
                headerLabelForOverView1.Text = "ACCOUNT -> SEARCH";
                ConstructSearchColumns();
                FillSearchGrid();
                dgvSearchList.ShowGridFilter = true;
                MMDatePanel.Visible = false;
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
        #endregion ShowOverview

        #region Other private methods
        //Construct Columns
        private void ConstructSearchColumns()
        {
            dgvSearchList.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccCode";
                column.DataPropertyName = "AccCode";
                column.HeaderText = "Type";
                column.Width = 90;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Account Name";
                column.Width = 200;
                column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccAddress";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.ReadOnly = true;
                column.Width = 290;
                dgvSearchList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //Fill Grid
        private void FillSearchGrid()
        {
            try
            {
                FillGridData();
                dgvSearchList.DataSource = _BindingSource;
                dgvSearchList.Bind();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillGridData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _Account.GetOverviewData();
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


