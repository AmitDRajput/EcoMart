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
using PharmaSYSPlus.CommonLibrary;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPatientSaleSearch : SearchControl
    {

        #region Declaration
        private DataTable _BindingSource;
        private SSSale _SSSale;
        #endregion

        #region Constructor
        public UclPatientSaleSearch()
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
                _SSSale = new SSSale();
                headerLabelForOverView1.Text = "PATIENT SALE SEARCH";
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
        public override int GetWidth()
        {
            int width = 20;
            try
            {               
                foreach (DataGridViewColumn col in dgvSearchList.Columns)
                {
                    if (col.Visible)
                        width += col.Width;
                }
              
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return width;
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
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 60;
                column.Visible = true;
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;             
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date ";
                column.Width = 80;             
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientName";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Patient Name";
                column.Width = 150;
                column.Visible = true;              
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientAddress";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 120;
                column.Visible = true;               
                dgvSearchList.Columns.Add(column);              

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = true;             
                dgvSearchList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Telephone";
                column.DataPropertyName = "Telephone";
                column.HeaderText = "Telephone";
                column.Width = 100;
                column.Visible = true;
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
                dgvSearchList.DateColumnNames.Add("Col_VoucherDate");
                dgvSearchList.DoubleColumnNames.Add("Col_Amount");
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
                dtable = _SSSale.GetOverviewData(FixAccounts.SubTypeForPatientSale);
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

