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
    public partial class UclPhoneBookSearch : SearchControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private PhoneBook _PhoneBook;
        #endregion

        #region Constructor
        public UclPhoneBookSearch()
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
        #endregion Constructor

        # region ShowOverview
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _PhoneBook = new PhoneBook();
                headerLabelForOverView1.Text = "PHONE BOOK -> SEARCH";
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
                column.Visible = false;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "Name";
                column.HeaderText = "Name";
                column.Width = 280;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "Address1";
                column.HeaderText = "Address";
                column.Width = 300;
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
                dtable = _PhoneBook.GetOverviewData();
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
