using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclLinkShelfProduct : BaseControl
    {
        # region Declaration   
        private ShelfProduct _ShelfProduct;
        # endregion Declaration

        # region Constructor
        public UclLinkShelfProduct()
        {
            try
            {
                InitializeComponent();
                _ShelfProduct = new ShelfProduct();
                SearchControl = new UclShelfSearch();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Constructor

        # region Initialize
        public void RefreshData()
        {
            try
            {
                FillShelfCombo();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Initialize

        # region IDetail Control
        public override void SetFocus()
        {
            mcbShelf.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _ShelfProduct.Initialise();
                ClearControls();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }
        public override bool Add()
        {
            bool retValue = false;
            try
            {
                retValue = base.Edit();
                ClearData();
                btnAdd.Visible = true;
                btnViewAll.Visible = false;
                headerLabel1.Text = "SHELF PRODUCT -> NEW";
                AddToolTip();
                FillShelfCombo();
                FillProductCombo();
                ConstructProdColumns();
                FillProdData();             
                dgvProduct.Enabled = false;
                tsBtnTrueFalse();
                mcbShelf.Enabled = true;                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void tsBtnTrueFalse()
        {
            if (_Mode == OperationMode.View)
            {
                tsBtnDelete.Visible = false;
                tsBtnEdit.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSearch.Visible = false;
                tsBtnAdd.Visible = false;
                tsBtnCancel.Visible = false;
                tsBtnFifth.Visible = false;
                tsBtnFirst.Visible = false;
                tsBtnLast.Visible = false;
                tsBtnNext.Visible = false;
                tsBtnPrevious.Visible = false;
            }
            else
            {
                tsBtnSavenPrint.Visible = false;
              
            }
        }
        public override bool Edit()
        {
            return true;
        }
        public override bool Cancel()
        {
            bool retValue = false;
            try
            {
                retValue = base.Cancel();
                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool Delete()
        {
            return true;
        }
        public override bool ProcessDelete()
        {
            return true;
        }
        public override bool View()
        {
            bool retValue = false;
            try
            {
                this.Cursor = Cursors.Default;
                retValue = base.View();
                btnAdd.Visible = false;
                FillShelfCombo();          
                ClearData();
                tsBtnTrueFalse();
                headerLabel1.Text = "DRUG GROUPING -> VIEW";
                this.Cursor = Cursors.Default;
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                _ShelfProduct.Id = mcbShelf.SelectedID;
                _ShelfProduct.Validate();

                if (_ShelfProduct.IsValid)
                {
                    _ShelfProduct.UpdateMasterProductClearShelfId(_ShelfProduct.Id);
                    UpdateProductMaster();
                    MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _ShelfProduct.Id;
                    mcbShelf.SelectedID = "";
                    retValue = true;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;

        }

        public bool UpdateProductMaster()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in dgvProduct.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null && prodrow.Cells["Col_ID"].Value.ToString() != "" )
                    {                       
                        _ShelfProduct.ProductID = Convert.ToInt32(prodrow.Cells["Col_ID"].Value.ToString());
                        returnVal = _ShelfProduct.UpdateMasterProductByProductID(_ShelfProduct.Id, _ShelfProduct.ProductID);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _ShelfProduct.Id = ID;
                    _ShelfProduct.ReadProdDetailsByShelfId(ID);              
                    FillProdData();
                    dgvProduct.Enabled = true;
                    mcbShelf.Enabled = false;
                    mcbShelf.Focus();
                }
                mcbShelf.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        #endregion IDetail Control

        #region IDetail Members

        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclShelf)
                    FillShelfCombo();
                else if (closedControl is UclProduct)
                    FillProdData();
                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Escape)
            {
                retValue = Exit();
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        # endregion IDetail Members

        #region other private methods        

        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[5] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack","ShelfCode" };
                mcbProduct.ColumnWidth = new string[5] { "0", "220", "50", "60","60" };
                mcbProduct.ValueColumnNo = 0;
                //    DataTable dtable = General.ProductList;
                Product prod = new Product();
                DataTable dtable = prod.GetOverviewData();
                
                mcbProduct.FillData(dtable);
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        private void FillShelfCombo()
        {
            try
            {
                mcbShelf.SelectedID = null;
                mcbShelf.SourceDataString = new string[2] { "ShelfID", "ShelfCode" };
                mcbShelf.ColumnWidth = new string[2] { "0", "200" };
                mcbShelf.ValueColumnNo = 0;
                mcbShelf.UserControlToShow = new UclShelf();
                Shelf _Shelf = new Shelf();
                DataTable dtable = _Shelf.GetOverviewData();
                mcbShelf.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ClearControls()
        {
            try
            {
                mcbShelf.Enabled = true;
                mcbShelf.SelectedID = null;
                mcbProduct.SelectedID = "";
                _ShelfProduct.Id = "";
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
                tsBtnCancel.Enabled = true;
                tsBtnSearch.Enabled = true;
                txtNoOfRows.Text = "";
                FillProdData();
                if (_Mode == OperationMode.View)
                {
                    mcbProduct.Visible = false;
                    mPlbl2.Visible = false;
                    btnAdd.Visible = false;
                }
                else
                {
                    mcbProduct.Visible = true;
                    mPlbl2.Visible = true;
                    btnAdd.Visible = true;
                }
                mcbShelf.Focus();
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillProdData()
        {
            try
            {
                dgvProduct.Refresh();
                DataTable dtable = new DataTable();
                if (mcbShelf.SelectedID != null && mcbShelf.SelectedID != "")
                    dtable = _ShelfProduct.ReadProdDetailsByShelfId(mcbShelf.SelectedID);
                dgvProduct.Rows.Clear();
                ConstructProdColumns();
                for (int index = 0; index < dtable.Rows.Count; index++)
                {
                    int rowIndex = dgvProduct.Rows.Add();
                    DataGridViewRow dr = dgvProduct.Rows[rowIndex];
                    dr.Cells[0].Value = dtable.Rows[index]["ProductID"].ToString();
                    dr.Cells[1].Value = dtable.Rows[index]["ProdName"].ToString();                 
                }
                if (dtable.Rows.Count > 0)
                    dgvProduct.Sort(dgvProduct.Columns[1], ListSortDirection.Ascending);
                NoofRows();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }
        private bool IsProductAlreadyAdded(int ProductID)
        {
            bool retValue = false;
            DataGridViewRow dr;
            try
            {
                for (int index = 0; index < dgvProduct.Rows.Count; index++)
                {
                    dr = dgvProduct.Rows[index];
                    if (Convert.ToInt32(dr.Cells[0].Value.ToString()) == ProductID)
                    {
                        retValue = true;
                        break;
                    }
                }
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
            return retValue;
        }
        private bool IsPartyAlreadyLinked(string Id)
        {
            bool retValue = false;
            try
            {
                DataTable dt = new DataTable();
                dt = null;
                dt = _ShelfProduct.IsPartyAlreadyLinked(Id);
                if (dt.Rows.Count > 0)
                    retValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        private void NoofRows()
        {
            int itemCount = 0;

            try
            {
                foreach (DataGridViewRow dr in dgvProduct.Rows)
                {
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        itemCount += 1;
                    }

                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region contruct grid

        private void ConstructProdColumns()
        {
            dgvProduct.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 600;
                column.ToolTipText = "Use Delete Key To Remove Product";
                dgvProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedBy";
                column.DataPropertyName = "CreatedUserID";
                column.Visible = false;
                dgvProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedDate";
                column.DataPropertyName = "CreatedDate";
                column.Visible = false;
                dgvProduct.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreatedTime";
                column.DataPropertyName = "CreatedTime";
                column.Visible = false;
                dgvProduct.Columns.Add(column);

            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }      

        # endregion

        # region Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                {
                    if (IsProductAlreadyAdded(Convert.ToInt32(mcbProduct.SelectedID)) == false)
                    {
                        int index = dgvProduct.Rows.Add();
                        DataGridViewRow dr = dgvProduct.Rows[index];
                        dr.Cells[0].Value = mcbProduct.SelectedID;
                        dr.Cells[1].Value = mcbProduct.SeletedItem.Text;
                        mcbProduct.SelectedID = "";
                    }
                    else
                    {
                        MessageBox.Show("Product Already Linked", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mcbProduct.SelectedID = "";
                    }
                }
                NoofRows();
                mcbProduct.Focus();
            }
            catch (Exception Ex) { Log.WriteException(Ex); }
        }      

        private void mcbShelf_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbShelf.SelectedID != null && mcbShelf.SelectedID != "")
                {
                    FillSearchData(mcbShelf.SelectedID,"");
                    mcbProduct.Focus();
                }
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

      

        # endregion

        #region tooltip
        private void AddToolTip()
        {
           
        }
        #endregion ToolTip

        private void mcbProduct_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                btnAdd.Focus();
        }

        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                btnAdd.Focus();
        }

    }
}

