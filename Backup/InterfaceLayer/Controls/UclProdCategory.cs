//Description : This class contains all methods required for product category master. 
//              This is user control required for Add/Update/Delete product category details


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
    public partial class UclProdCategory :BaseControl 
    {
        # region Declaration       
        private ProductCategory _ProductCategory;       
        # endregion

        # region Constructor

        public UclProdCategory()
        {
            InitializeComponent();
            _ProductCategory = new ProductCategory();
            SearchControl = new UclProdCategorySearch();
        }    
        # endregion               

        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _ProductCategory.Initialise();
            ClearControls();
            txtName.Focus();
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            panel1.Enabled = true;
            FilltxtName();
            AddToolTip();
            headerLabel1.Text = "PRODUCT CATEGORY -> NEW";
            txtName.Focus();
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            AddToolTip();
            headerLabel1.Text = "PRODUCT CATEGORY -> EDIT";
            FilltxtName();
            txtName.Focus();
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            panel1.Enabled = true;
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "PRODUCT CATEGORY -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;          
            if (_ProductCategory.Id != null && _ProductCategory.Id != "")
            {
                retValue = _ProductCategory.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _ProductCategory.DeleteDetails();
                    MessageBox.Show("ProductCategory information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            panel1.Enabled = true;
            ClearData();
            FilltxtName();
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            FilltxtName();
            headerLabel1.Text = "PRODUCT CATEGORY -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _ProductCategory.Name = txtName.Text;
            _ProductCategory.IFSaleDiscount = txtSaleDiscount.Text;
            if (_Mode == OperationMode.Edit)
                _ProductCategory.IFEdit = "Y";
            _ProductCategory.Validate();
            if (_ProductCategory.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _ProductCategory.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _ProductCategory.CreatedBy = General.CurrentUser.Id;
                    _ProductCategory.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _ProductCategory.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _ProductCategory.AddDetails();
                    retValue = _ProductCategory.UpdateProductMaster();                
                    MessageBox.Show("ProductCategory information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _ProductCategory.Id;
                    ClearControls();
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _ProductCategory.ModifiedBy = General.CurrentUser.Id;
                    _ProductCategory.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _ProductCategory.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _ProductCategory.UpdateDetails();
                    retValue = _ProductCategory.UpdateProductMaster();                    
                    MessageBox.Show("ProductCategory information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _ProductCategory.Id;
                    ClearControls();
                    retValue = true;
                }
            }
            else // Show Validation ProductCategorys
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _ProductCategory.ValidationMessages)
                {
                    _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                }
                MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                _ProductCategory.Id = ID;
                _ProductCategory.ReadDetailsByID();
                txtName.Text = _ProductCategory.Name;
                txtSaleDiscount.Text = _ProductCategory.IFSaleDiscount;
                txtName.Focus();
            }

            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "ProductCategoryID", "ProductCategoryName" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            ProductCategory _txt = new ProductCategory();
            DataTable dtable = _txt.GetOverviewData();
            txtName.FillData(dtable);
        }

        # endregion Idetail Control

        #region IDetail Members
        public override void ReFillData()
        {
            FilltxtName();
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

        #region other Private Methods


        public override bool IsDetailChanged()
        {
            bool retValue = false;
            if (_ProductCategory.Name != null && _ProductCategory.Name != txtName.Text)
                retValue = true;
            return retValue;
        }
        private void ClearControls()
        {
            txtName.Text = "";
            txtName.SelectedID = "";
            txtName.Focus();
            txtSaleDiscount.Text = "";
            _ProductCategory.IFSaleDiscount = "";
        }
        #endregion

        # region Events

        private void txtName_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (txtName.SelectedID != null && txtName.SelectedID != "")
            {
                FillSearchData(txtName.SelectedID,"");
                txtSaleDiscount.Focus();
            }
        }
        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            txtSaleDiscount.Focus();
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttProductCategory.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion     

       
    }
}
