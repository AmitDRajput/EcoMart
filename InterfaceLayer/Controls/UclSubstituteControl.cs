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
using EcoMart.DataLayer;


namespace EcoMart.InterfaceLayer.Controls
{
    public partial class UclSubstituteControl : UserControl
    {
        #region declaration

        private DataTable _BindingSource;
        public string _drugCode = "";

        private Substitute _Substitute; public delegate void ProductSelected(string productID);
        public event ProductSelected OnProductSelected;

        #endregion

        #region Constructor


        public UclSubstituteControl()
        {
            InitializeComponent();
            _Substitute = new Substitute();
        }

        #endregion Constructor

        #region IDetail Control
        public void SetFocus()
        {
            mcbProduct.Focus();
        }
        public void Initialize()
        {
            _Substitute.Initialise();
            FillProductCombo();
            FillGenericCategoryCombo();
            InitializeReportGrid();
            dgvSubstitute.Visible = false;
            mcbProduct_EnterKeyPressed();
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
                //    DataTable dtable = General.ProductList;
                Product prod = new Product();
                DataTable dtable = prod.GetOverviewData();
                mcbProduct.FillData(dtable);
                mcbProduct.SelectedID = General.SubstituteProductID; // [ansuman]
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion IDetail Control

        #region Internal methods

        internal bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (modifier == Keys.Control && keyPressed == Keys.P)
            {
                mcbProduct.Focus();
                retValue = true;
            }
            else if (modifier == Keys.Control && keyPressed == Keys.I)
            {
                mcbGenericCategory.Focus();
                retValue = true;
            }
            else if (modifier == Keys.Control && keyPressed == Keys.R)
            {
                mcbProduct.SelectedID = null;
                mcbGenericCategory.SelectedID = null;
                dgvSubstitute.Visible = false;
                mcbProduct.Focus();
                retValue = true;
            }
            return retValue;
        }

        private void FillGenericCategoryCombo()
        {
            try
            {
                mcbGenericCategory.SelectedID = null;
                mcbGenericCategory.SourceDataString = new string[2] { "GenericCategoryID", "GenericCategoryName" };
                mcbGenericCategory.ColumnWidth = new string[2] { "0", "600" };
                mcbGenericCategory.ValueColumnNo = 0;
                mcbGenericCategory.UserControlToShow = new UclGenericCategory();
                GenericCategory genc = new GenericCategory();
                DataTable dtable = genc.GetOverviewData();
                mcbGenericCategory.FillData(dtable);
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
                column.Width = 50;
                column.ReadOnly = true;
                dgvSubstitute.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Com";
                column.Width = 50;
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
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
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
        private void ShowDrugName(string _drugCode)
        {
            //string nm = _Substitute.GetDrugName(_drugCode);
            //mcbGenericCategory.

        }
        private void FillSubstituteGrid()
        {
            try
            {
                _BindingSource = _Substitute.GetOverviewDataByDrugCode(_drugCode);

                string query = "a.ProdName = '" + SProdName + "'";
                DataTable dtClosingStock = DBProduct.GetFilteredProductStock(query);
                DataTable dtMergedProduct = DBProduct.GetMergedSimillarProductStock(_BindingSource, dtClosingStock);


                dgvSubstitute.DataSource = dtMergedProduct;
                dgvSubstitute.Bind();
                dgvSubstitute.Select();
                dgvSubstitute.Focus();
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion Internal methods

        #region "Events"

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
        private void UclSubstituteControl_Load(object sender, EventArgs e)
        {
            mcbProduct.Focus();
        }
        public static string SProdName = "";
        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbProduct_EnterKeyPressed();
        }

        private void mcbProduct_EnterKeyPressed()
        {
            try
            {
                if (mcbProduct.SeletedItem != null && string.IsNullOrEmpty(mcbProduct.SelectedID) == false)
                {
                    _drugCode = mcbProduct.SeletedItem.ItemData[5];
                    SProdName = mcbProduct.SeletedItem.Text;
                    if (string.IsNullOrEmpty(_drugCode) == false)
                    {
                        mcbGenericCategory.SelectedID = _drugCode;
                        ShowDrugName(_drugCode);
                        dgvSubstitute.Visible = true;
                        FillSubstituteGrid();
                    }
                    if (dgvSubstitute.Rows.Count > 0)
                    {
                        dgvSubstitute.Focus();
                        dgvSubstitute.Select();
                        dgvSubstitute.Rows[0].Selected = true;
                    }
                    else
                        mcbProduct.Focus();
                }
                else
                {
                    mcbGenericCategory.Focus();
                    dgvSubstitute.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void mcbGenericCategory_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbGenericCategory.SeletedItem != null)
            {
                if (string.IsNullOrEmpty(Convert.ToString(mcbGenericCategory.SeletedItem.ItemData[0])) == false)
                {
                    mcbProduct.SelectedID = null;
                    _drugCode = mcbGenericCategory.SeletedItem.ItemData[0];
                    if (string.IsNullOrEmpty(_drugCode) == false)
                    {
                        dgvSubstitute.Visible = true;
                        FillSubstituteGrid();
                    }
                    if (dgvSubstitute.Rows.Count > 0)
                    {
                        dgvSubstitute.Focus();
                        dgvSubstitute.Select();
                        dgvSubstitute.Rows[0].Selected = true;
                    }
                    else
                    {
                        mcbGenericCategory.Focus();
                    }
                }
                else
                {
                    mcbGenericCategory.Focus();
                    dgvSubstitute.Visible = false;
                }
            }
        }
        private void mcbGenericCategory_UpArrowPressed(object sender, EventArgs e)
        {
            mcbProduct.Focus();
        }

        private void UclSubstituteControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
            {
                mcbProduct.Focus();
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                mcbGenericCategory.Focus();
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
            {
                mcbProduct.SelectedID = null;
                mcbGenericCategory.SelectedID = null;
                mcbProduct.Focus();
            }
        }

        #endregion "Events"
    }
}
