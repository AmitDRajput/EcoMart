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
    public partial class UclCompanySearch : SearchControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Company _Company;       
        #endregion

        #region Constructor
        public UclCompanySearch()
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
                _Company = new Company();
                headerLabelForOverView1.Text = "COMPANY -> SEARCH";
                ConstructSearchColumns();
                dgvSearchList.ShowGridFilter = true;
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
                column.DataPropertyName = "CompId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompShortName";
                column.DataPropertyName = "CompShortName";
                column.HeaderText = "Short";
                column.Width = 60;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompName";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Company Name";
                column.Width = 300;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompContactPerson";
                column.DataPropertyName = "CompContactPerson";
                column.HeaderText = "Contact Person";
                column.Width = 220;
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
                dtable = _Company.GetOverviewData();
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
