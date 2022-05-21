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
using EcoMart.Common;
using EcoMart.BusinessLayer;
using System.Collections;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclProdCategory :BaseControl 
    {
        # region Declaration       
        private ProductCategory _ProductCategory;
        Hashtable htTableList;
        public int CurrentNumber = 0;
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
            htTableList = General.GetTableListByCode("productcategoryID", "productcategoryName", "Masterproductcategory");
            bool retValue = base.View();
            ClearData();
            FilltxtName();
            headerLabel1.Text = "PRODUCT CATEGORY -> VIEW";
            MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _ProductCategory.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_ProductCategory.Id, "");

            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            try
            {
                CurrentNumber = htTableList.Count;
                if (htTableList.Contains(CurrentNumber))
                    _ProductCategory.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_ProductCategory.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            CurrentNumber -= 1;
            if (htTableList.Contains(CurrentNumber))
                _ProductCategory.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_ProductCategory.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _ProductCategory.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_ProductCategory.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _ProductCategory.Name = txtName.Text;
            //_ProductCategory.IFSaleDiscount = "N";
            //_ProductCategory.LBTPercent = 0;
            //_ProductCategory.IFDoctorRequired = "N";
            if (_Mode == OperationMode.Edit)
                _ProductCategory.IFEdit = "Y";
            _ProductCategory.Validate();
            if (_ProductCategory.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _ProductCategory.CreatedBy = General.CurrentUser.Id;
                    _ProductCategory.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _ProductCategory.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    //retValue = _ProductCategory.AddDetails();
                    ////retValue = _ProductCategory.UpdateProductMaster();                
                    //MessageBox.Show("ProductCategory information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //_SavedID = _ProductCategory.Id;
                    //ClearControls();
                    //retValue = true;
                    _ProductCategory.IntID = _ProductCategory.AddDetails();
                    if (_ProductCategory.IntID > 0)
                    {
                        MessageBox.Show("Product Category information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SaveIntID = _ProductCategory.IntID;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }
                    else
                    {
                        retValue = false;
                    }
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _ProductCategory.ModifiedBy = General.CurrentUser.Id;
                    _ProductCategory.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _ProductCategory.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _ProductCategory.UpdateDetails();                                    
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
        public override void ReFillData(Control closedControl)
        {
            //FilltxtName();
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
            txtLBTPercent.Text = "0.0";
            txtDoctorRequired.Text = "";
            txtName.Focus();
            txtSaleDiscount.Text = "";
            tsBtnFifth.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSavenPrint.Visible = false;
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

        private void txtSaleDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLBTPercent.Focus();
            else if (e.KeyCode == Keys.Up)
                txtName.Focus();
        }

        private void txtLBTPercent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDoctorRequired.Focus();
            else if (e.KeyCode == Keys.Up)
                txtSaleDiscount.Focus();
        }

        private void txtDoctorRequired_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtName.Focus();
        }
    }
}
