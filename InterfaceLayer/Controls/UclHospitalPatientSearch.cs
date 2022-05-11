using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;

namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclHospitalPatientSearch : SearchControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private HospitalPatient _HospitalPatient;
        # endregion Declaration

        # region Constructor
        public UclHospitalPatientSearch()
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
        # endregion Constructor

        #region IOverview Members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _HospitalPatient = new HospitalPatient();
                headerLabelForOverView1.Text = "ADMITTED PATIENT -> SEARCH";
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


        # endregion

        # region Other Private Methods

        //Construct Columns
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
                column.Name = "Col_InWardNumber";
                column.DataPropertyName = "InWardNumber";
                column.HeaderText = "InWardNumber";
                column.Width = 80;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Name";
                column.Width = 200;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "Address1";
                column.HeaderText = "Address";
                column.Width = 150;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "Telephone";
                column.HeaderText = "Telephone";
                column.Width = 100;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RoomNumber";
                column.DataPropertyName = "RoomNumber";
                column.HeaderText = "RoomNumber";
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
                dtable = _HospitalPatient.GetOverviewData();
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion

        # region Events

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

        # endregion
    }
}
