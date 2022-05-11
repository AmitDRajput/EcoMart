using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using EcoMart.InterfaceLayer;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclUserRightsSearch : SearchControl
    {
        #region Declration
        private UserRights _Right;
        private DataTable _BindingSource;

        #endregion

        #region Constructor
        public UclUserRightsSearch()
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
                _Right = new UserRights();
                headerLabelForOverView1.Text = "USER RIGHTS -> SEARCH";
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
        #endregion IOverview

        #region Other private methods
        private void ConstructSearchColumns()
        {
            dgvSearchList.Columns.Clear();
            DataGridViewTextBoxColumn column;
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "ID";
                column.Width = 100;
                column.Visible = false;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_FormName";
                column.DataPropertyName = "FormName";
                column.HeaderText = "FormName";
                column.Width = 180;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Add";
                column.DataPropertyName = "AddModule";
                column.HeaderText = "Add";
                column.Width = 80;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Edit";
                column.DataPropertyName = "EditModule";
                column.HeaderText = "Edit";
                column.Width = 80;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Delete";
                column.DataPropertyName = "DeleteModule";
                column.HeaderText = "Delete";
                column.Width = 80;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_View";
                column.DataPropertyName = "ViewModule";
                column.HeaderText = "View";
                column.Width = 80;
                //column.ReadOnly = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Print";
                column.DataPropertyName = "PrintModule";
                column.HeaderText = "Print";
                column.Width = 80;
                //column.ReadOnly = true;
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
                //Get all users
                dtable = _Right.GetOverviewData();
                DataTable table = new DataTable();
                table = ConvertAuthorityInfo(dtable);
                _BindingSource = table;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private DataTable ConvertAuthorityInfo(DataTable dtable)
        {
            int rcnt, ccnt, i, j;
            DataRow dtrow = null;
            try
            {
                rcnt = dtable.Rows.Count;
                ccnt = dtable.Columns.Count;
                for (i = 0; i < rcnt; i++)
                {
                    dtrow = dtable.Rows[i];
                    for (j = 2; j < ccnt; j++)
                    {
                        string ro = dtrow[j].ToString();
                        string value = GetLevelInfo(ro);
                        dtrow[j] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dtable;
        }
        private string GetLevelInfo(string ro)
        {
            string value = "";
            int lcnt;
            DataRow dtrow = null;
            DataTable Level = new DataTable();
            try
            {
                Level = _Right.GetLevelData();
                lcnt = Level.Rows.Count;
                int no = Convert.ToInt32(ro);
                dtrow = Level.Rows[no];
                switch (ro)
                {
                    case "0": value = dtrow["Type"].ToString();
                        break;
                    case "1": value = dtrow["Type"].ToString();
                        break;
                    case "2": value = dtrow["Type"].ToString();
                        break;
                    case "3": value = dtrow["Type"].ToString();
                        break;
                    case "4": value = dtrow["Type"].ToString();
                        break;
                    case "5": value = dtrow["Type"].ToString();// for futuer use
                        break;
                    case "6": value = dtrow["Type"].ToString();// for futuer use
                        break;
                    case "7": value = dtrow["Type"].ToString();// for futuer use
                        break;

                    // default: value = "Invalid";
                    //break;

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

            return value;
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
