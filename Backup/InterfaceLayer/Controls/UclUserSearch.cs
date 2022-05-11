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
    public partial class UclAddUserSearch : SearchControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private User _User;
        #endregion

        #region Constructor
        public UclAddUserSearch()
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
                _User = new User();
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
                column.DataPropertyName = "UserId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UserName";
                column.DataPropertyName = "UserName";
                column.HeaderText = "Users";
                column.Width = 175;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Password";
                column.DataPropertyName = "Password";
                column.HeaderText = "Password";
                column.Width = 150;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Level";
                column.DataPropertyName = "Level";
                column.HeaderText = "Level";
                column.Width = 175;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfInUse";
                column.DataPropertyName = "IfInUse";
                column.HeaderText = "IfInUse";
                column.Width = 80;
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
                dtable = _User.GetOverviewData();
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
            int rcnt, ccnt, i;
            DataRow dtrow = null;
            try
            {
                rcnt = dtable.Rows.Count;
                ccnt = dtable.Columns.Count;
                for (i = 0; i < rcnt; i++)
                {
                    dtrow = dtable.Rows[i];
                    string ro = dtrow["Level"].ToString();
                    string value = GetLevelInfo(ro);
                    dtrow["Level"] = value;
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
                Level = _User.GetLevelData();
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

        private void dgvUserList_OnCellFormattingEvent(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4) //Column If in use
                {
                    int val;
                    int.TryParse(e.Value.ToString(), out val);
                    if (val == 1)
                    {
                        e.Value = "YES";
                    }
                    else
                    {
                        e.Value = "NO";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

    }
}
