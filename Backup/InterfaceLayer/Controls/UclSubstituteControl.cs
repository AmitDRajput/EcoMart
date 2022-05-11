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

namespace PharmaSYSRetailPlus.InterfaceLayer.Controls
{
    public partial class UclSubstituteControl : UserControl
    {
        public delegate void ProductSelected(string productID);
        public event ProductSelected OnProductSelected;
        
        # region declaration
        private DataTable _BindingSource;
        public string _drugCode = "";
        private Substitute _Substitute;
        #endregion
        
        public void SetFocus()
        {
            mcbProduct.Focus();
        }

        public UclSubstituteControl()
        {
            InitializeComponent();
            _Substitute = new Substitute();
        }

        public void Initialize()
        {
            _Substitute.Initialise();
            FillProductCombo();
            InitializeReportGrid();
        }     

        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvSubstitute.Columns["Col_ID"].Visible = false;
            FormatReportGrid();
            dgvSubstitute.InitializeColumnContextMenu();
        }

        private void FormatReportGrid()
        {
            dgvSubstitute.DoubleColumnNames.Add("Col_Margin");
            dgvSubstitute.OptionalColumnNames.Add("Col_Margin");
        }
        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[6] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName", "ProdDrugID" };
                mcbProduct.ColumnWidth = new string[6] { "0", "200", "50", "50", "50", "0" };
                mcbProduct.ValueColumnNo = 0;
                mcbProduct.UserControlToShow = new UclProduct();
                DataTable dtable = General.ProductList;
                mcbProduct.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void ConstructReportColumns()
        {
            try
            {
                dgvSubstitute.Columns.Clear();
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                dgvSubstitute.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.Visible = false;
                dgvSubstitute.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 170;
                column.ReadOnly = true;
                dgvSubstitute.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                dgvSubstitute.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                dgvSubstitute.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.ReadOnly = true;
                dgvSubstitute.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Quantity";
                column.Width = 70;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSubstitute.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";
                column.DataPropertyName = "Margin";
                column.HeaderText = "Margin";
                column.Width = 70;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSubstitute.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void mcbProduct_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
            {
                _drugCode = mcbProduct.SeletedItem.ItemData[5];
                if (_drugCode != null && _drugCode != "")
                    FillSubstituteGrid();
            }
        }

        private void FillSubstituteGrid()
        {
            _BindingSource = _Substitute.GetOverviewDataByDrugCode(_drugCode);
            dgvSubstitute.DataSource = _BindingSource;
            dgvSubstitute.Bind();
        }

        private void dgvSubstitute_DoubleClicked(object sender, EventArgs e)
        {
            if (OnProductSelected != null)
            {
                Visible = false;
                if (dgvSubstitute.SelectedRow != null)
                    OnProductSelected(dgvSubstitute.SelectedRow.Cells["Col_ProductID"].Value.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancelClick();
        }

        private void btnCancelClick()
        {
            this.Visible = false;
        }

        private void btnCancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCancelClick();
            }
        }
    }
}
