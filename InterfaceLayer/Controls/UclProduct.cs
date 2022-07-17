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
using System.Text.RegularExpressions;
using EcoMart.InterfaceLayer.CommonControls;
using EcoMart.InterfaceLayer.Classes;
using System.Collections;
using EcoMart.DataLayer;
using PaperlessPharmaRetail.Common.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclProduct : BaseControl
    {
        #region Declaration

        private Product _Product;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        private string Prod_SelectedID = string.Empty;//kiran
        PartyCompany pc;

        #endregion

        #region  Constructor
        public UclProduct()
        {
            try
            {
                InitializeComponent();
                _Product = new Product();
                SearchControl = new UclProductSearch();
                pc = new PartyCompany();
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }

        }
        #endregion

        # region IDetailControl
        public override void SetFocus()
        {
            this.ActiveControl = txtName;
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Product.Initialise();
            ClearControls();
            return true;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            AddToolTip();
            headerLabel1.Text = "PRODUCT -> NEW";
            Fillmcb();
            FillPack();
            FillPackType();
            //txtLoosePack.Text = _Product.ProdLoosePack.ToString().Trim();
            txtName.Focus();
            Prod_SelectedID = string.Empty;
            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            Fillmcb();
            FillPack();
            FillPackType();
            headerLabel1.Text = "PRODUCT -> EDIT";
            txtName.Focus();
            AddToolTip();
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            panel2.Enabled = true;
            return true;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "PRODUCT -> DELETE";
            ClearData();
            Fillmcb();
            FillPack();
            FillPackType();
            txtName.Focus();
            panel2.Enabled = true;
            return retValue;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_Product.Id != null && _Product.Id != "")
            {
                retValue = _Product.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Product.DeleteDetails();
                    if (retValue)
                    {
                        MessageBox.Show("Product information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //   General.NotifyProductListRefill();                    

                        FilltxtName();
                    }
                    else
                        MessageBox.Show("Cannot Delete...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            panel2.Enabled = true;
            ClearData();
            Fillmcb();
            txtName.Focus();
            return true;
        }

        public override bool Exit()
        {
            bool retValue = base.Exit();
            return retValue;
        }

        public override bool View()
        {
            htTableList = General.GetTableListByCode("ProductID", "ProdName", "MasterProduct");
            bool retValue = base.View();
            panel2.Enabled = true;
            ClearData();
            Fillmcb();
            txtName.Focus();
            headerLabel1.Text = "PRODUCT -> VIEW";
            if (Mode == OperationMode.Delete || Mode == OperationMode.View)
                panel2.Enabled = false;
            //MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Product.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Product.Id, "");

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
                    _Product.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Product.Id, "");
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
                _Product.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Product.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Product.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Product.Id, "");
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            if (ValidateData() == false)
            {
                return false;
            }
            System.Text.StringBuilder _errorMessage;
            _Product.Name = txtName.Text.Trim();
            _Product.Name = (_Product.Name.Replace("*", "X"));
            _Product.Name = (_Product.Name.Replace("%", "Per"));
            if (mcbCompany.SelectedID != null)
            {
                _Product.ProdCompID = Convert.ToInt32(mcbCompany.SelectedID.Trim());
                _Product.ProdCreditor1ID = Convert.ToInt32(mcbCompany.SeletedItem.ItemData[3]);
            }
            _Product.ProdCompShortName = txtCompShortName.Text.Trim();

            _Product.ProdLoosePack = 1;
            _Product.ProdPack = txtPack.Text.Trim();
            if (txtPack.SelectedID != null)
                _Product.ProdPackID = Convert.ToInt32(txtPack.SelectedID);
            else
                _Product.ProdPackID = 0;

            _Product.ProdPackType = txtPackType.Text.Trim();
            if (txtPackType.SelectedID != null)
                _Product.ProdPackTypeID = Convert.ToInt32(txtPackType.SelectedID);
            else
                _Product.ProdPackTypeID = 0;

            if (txtVAT.Text.Trim() != "")
                _Product.ProdVATPer = Convert.ToDouble(txtVAT.Text.Trim());
            //if (txtCST.Text.Trim() != "")
            //    _Product.ProdCST = Convert.ToDouble(txtCST.Text.Trim());
            if (txtMaxLevel.Text.Trim() != "")
                _Product.ProdMaxLevel = Convert.ToInt32(txtMaxLevel.Text.Trim());
            if (txtMinLevel.Text.Trim() != "")
                _Product.ProdMinLevel = Convert.ToInt32(txtMinLevel.Text.Trim());
            if (txtBoxQty.Text.Trim() != "")
                _Product.ProdBoxQty = Convert.ToInt32(txtBoxQty.Text.Trim());
            if (mcbGenericCategory.SeletedItem != null)
                _Product.ProdGenericID = Convert.ToInt32(mcbGenericCategory.SeletedItem.Value.Trim());

            if (mcbShelfCode.SelectedID != "" && mcbShelfCode.SelectedID != null)
                _Product.ProdShelfID = Convert.ToInt32(mcbShelfCode.SelectedID.Trim());
            if (mcbProductCategory.SelectedID != null)
                _Product.ProdProductCategoryID = Convert.ToInt32(mcbProductCategory.SelectedID.Trim());
            _Product.ProdRequireColdStorage = txtRequireColdStorage.Text.ToString();
            _Product.HSNNumber = Convert.ToInt32(txtHSNNumber.Text.ToString());
            if (_Mode == OperationMode.Edit)
                _Product.IFEdit = "Y";
            else
                _Product.IFEdit = "N";
            _Product.Validate();
            if (_Product.IsValid)
            {
                if (_Product.ProdPackID == 0)
                {
                    _Product.ProdPackID = Convert.ToInt32(_Product.SearchForProdPack(_Product.ProdPack).ToString());
                    if (_Product.ProdPackID == 0)
                    {
                        //SS
                        _Product.IfNewPack = "Y";
                        //_Product.ProdPackID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    }
                }
                if (_Product.ProdPackType != null && _Product.ProdPackType != string.Empty)
                {
                    _Product.ProdPackTypeID = _Product.SearchForProdPackType(_Product.ProdPackType);
                    if (_Product.ProdPackTypeID.ToString() == null || _Product.ProdPackTypeID == 0)
                    {
                        _Product.IfNewPackType = "Y";
                        //_Product.ProdPackTypeID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    }
                }
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _Product.IntID = _Product.GetNextIntID();
                    _Product.CreatedBy = General.CurrentUser.Id;
                    _Product.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Product.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    if (_Product.IntID > 0)
                        retValue = _Product.AddDetails();

                    if (_Product.IfNewPack != "" && _Product.IfNewPack == "Y")
                    {
                        _Product.ProdPackID = _Product.GetNextPackID();
                        retValue = _Product.AddPack();
                    }
                    if (_Product.IfNewPackType != "" && _Product.IfNewPackType == "Y")
                    {
                        _Product.ProdPackTypeID = _Product.GetNextPackTypeID();
                        retValue = _Product.AddPackType();
                    }

                    //retValue = _Product.RemoveProductDrugLink();
                    //SS
                    // TOCHECK
                    //////////////////_Product.ProdLinkDrugId = _Product.GetIntID(); /* Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/
                    //////////////////if (_Product.ProdGenericID.ToString() != null && _Product.ProdGenericID.ToString() != string.Empty && _Product.ProdGenericID != 0)
                    //////////////////    retValue = _Product.SaveProductDrugLink();


                    _Product.IfNewPack = "N";
                    _Product.IfNewPackType = "N";

                    //AddInLinkPartyCompany();

                    MessageBox.Show("Product information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Product.Id;
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {

                    _Product.ModifiedBy = General.CurrentUser.Id;
                    _Product.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Product.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Product.UpdateDetails();

                    //_Product.ProdLinkDrugId = _Product.GetIntID(); /* Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/
                    //if (_Product.ProdGenericID.ToString () != null && _Product.ProdGenericID != 0)
                    //    retValue = _Product.SaveProductDrugLink();

                    MessageBox.Show("Product information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Product.Id;
                    retValue = true;
                }

                CacheObject.Clear("cacheCounterSale");
                _Product.GetOverviewData();
            }
            else
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Product.ValidationMessages)
                {
                    _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                }
                MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retValue;
        }

        private bool AddInLinkPartyCompany() // [ansuman- 13.04.2017]
        {
            List<PSComboBoxNew> Creditorlist = new List<PSComboBoxNew>();

            foreach (PSComboBoxNew items in Creditorlist)
            {
                string UniqueID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                pc.Id = items.SelectedID;
                pc.DetailId = UniqueID;
                pc.CompanyId = mcbCompany.SelectedID;
                pc.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                pc.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                pc.CreatedBy = General.CurrentUser.Id;
                pc.ModifiedBy = "";
                pc.ModifiedDate = "";
                pc.ModifiedTime = "";

                bool retValue = pc.AddDetails();
            }
            return true;
        }

        private bool ValidateData()
        {
            if (ValidateEmptyorNullData(txtName.Text, "Please enter Product Name.") == true)
            {
                txtName.Focus();
                return false;
            }
            else if (mcbCompany.SeletedItem == null || string.IsNullOrEmpty(mcbCompany.SeletedItem.Text) == true)
            {
                ValidateEmptyorNullData(string.Empty, "Please enter Company Name.");
                mcbCompany.Focus();
                return false;
            }
            else if (ValidateEmptyorNullData(txtCompShortName.Text, "Please enter Company short Name.") == true)
            {
                txtCompShortName.Focus();
                return false;
            }
            //else if (ValidateEmptyorNullData(txtLoosePack.Text, "Please enter Unit of measure.") == true)
            //{
            //    txtLoosePack.Focus();
            //    return false;
            //}
            else if (ValidateEmptyorNullData(txtPack.Text, "Please enter Pack.") == true)
            {
                txtPack.Focus();
                return false;
            }
            else if (mcbProductCategory.SeletedItem == null || string.IsNullOrEmpty(mcbProductCategory.SeletedItem.Text) == true)
            {
                ValidateEmptyorNullData(string.Empty, "Please enter Product Category.");
                mcbProductCategory.Focus();
                return false;
            }
            else if (ValidateEmptyorNullData(txtPackType.Text, "Please enter Packing Type.") == true)
            {
                txtPackType.Focus();
                return false;
            }
            return true;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                _Product.Id = ID;
                _Product.ReadDetailsByID();
                if (_Mode != OperationMode.Edit)
                    txtName.SelectedID = _Product.Id;
                txtCompShortName.Text = _Product.ProdCompShortName;
                txtName.Text = _Product.Name;
                //txtLoosePack.Text = Convert.ToString(_Product.ProdLoosePack);

                txtPack.Text = _Product.ProdPack;
                txtPackType.Text = _Product.ProdPackType;

                txtMinLevel.Text = Convert.ToString(_Product.ProdMinLevel);
                txtMaxLevel.Text = Convert.ToString(_Product.ProdMaxLevel);
                txtBoxQty.Text = Convert.ToString(_Product.ProdBoxQty);
                txtVAT.Text = _Product.ProdVATPer.ToString("#0.00");
                //txtCST.Text = _Product.ProdCST.ToString("#0.00");
                mcbCompany.SelectedIntID = Convert.ToInt32(_Product.ProdCompID);
                mcbGenericCategory.SelectedIntID = _Product.ProdGenericID;
                mcbProductCategory.SelectedIntID = _Product.ProdProductCategoryID;
                mcbShelfCode.SelectedIntID = _Product.ProdShelfID;
                txtHSNNumber.Text = Convert.ToString(_Product.HSNNumber);
                txtName.Focus();
            }
            if (Mode == OperationMode.Delete || Mode == OperationMode.View)
                panel2.Enabled = false;
            return true;
        }

        #endregion IDetailControl

        #region IDetail Members

        public override void ReFillData(Control closedControl)
        {
            if (closedControl is UclCompany)
                FillCompanyCombo();
            else if (closedControl is UclGenericCategory)
                FillGenericCategoryCombo();
            else if (closedControl is UclShelf)
                FillShelfCombo();
            else if (closedControl is UclProdCategory)
                FillProdCategoryCombo();
            FillPack();
            FillPackType();
        }
        public override bool RefreshProductList()
        {
            string preselectedID = "";
            if (txtName.SelectedID != null)
            {
                preselectedID = txtName.SelectedID;
            }
            FilltxtName();
            txtName.SelectedID = preselectedID;
            return true;
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.C && modifier == Keys.Alt)
            {
                mcbCompany.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.S && modifier == Keys.Alt)
            {
                txtCompShortName.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.N && modifier == Keys.Alt)
            {
                txtName.Focus();
                retValue = true;
            }
            //if (keyPressed == Keys.U && modifier == Keys.Alt)
            //{
            //    txtLoosePack.Focus();
            //    retValue = true;
            //}
            if (keyPressed == Keys.K && modifier == Keys.Alt)
            {
                txtPack.Focus();
                retValue = true;
            }

            if (keyPressed == Keys.V && modifier == Keys.Alt)
            {
                txtVAT.Focus();
                retValue = true;
            }

            if (keyPressed == Keys.I && modifier == Keys.Alt)
            {
                txtMinLevel.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.X && modifier == Keys.Alt)
            {
                txtMaxLevel.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.Q && modifier == Keys.Alt)
            {
                txtBoxQty.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.Y && modifier == Keys.Alt)
            {
                mcbGenericCategory.Focus();
                retValue = true;

            }
            if (keyPressed == Keys.L && modifier == Keys.Alt)
            {
                mcbShelfCode.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.P && modifier == Keys.Alt)
            {
                mcbProductCategory.Focus();
                retValue = true;
            }

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
        # endregion IDetail Member

        #region Other Private Methods

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        private void ClearControls()
        {
            txtName.Text = "";
            txtName.SelectedID = "";
            txtCompShortName.Text = "";
            txtVAT.Text = "12.0";
            //txtCST.Text = "";
            //txtLoosePack.Text = "";
            txtPack.Text = "";
            txtPackType.Text = "";
            txtMaxLevel.Text = "";
            txtMinLevel.Text = "";
            txtBoxQty.Text = "";
            txtCompShortName.Text = "";
            Clearmcb();
            lblMessage.Text = "";
            txtRequireColdStorage.Text = "N";
            panel2.Enabled = true;
            tsBtnFifth.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSavenPrint.Visible = false;
        }

        private void Clearmcb()
        {
            txtName.SelectedID = "";
            mcbCompany.SelectedID = "";
            //mcbFirstCreditor.SelectedID = "";
            mcbGenericCategory.SelectedID = "";
            mcbProductCategory.SelectedID = "";
            //mcbSecondCreditor.SelectedID = "";
            mcbShelfCode.SelectedID = "";
        }



        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[5] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName" };
            txtName.ColumnWidth = new string[5] { "0", "315", "50", "50", "50" };
            // DataTable dtable = General.ProductList;           
            DataTable dtable = _Product.GetOverviewData();
            txtName.FillData(dtable);
        }
        private void Fillmcb()
        {
            FilltxtName();
            FillCompanyCombo();
            FillGenericCategoryCombo();
            FillShelfCombo();
            FillProdCategoryCombo();
        }
        private void FillCompanyCombo()
        {
            mcbCompany.SelectedID = null;
            mcbCompany.SourceDataString = new string[5] { "CompID", "CompName", "CompShortName", "PartyID_1", "PartyID_2" };
            mcbCompany.ColumnWidth = new string[5] { "0", "250", "50", "0", "0" };
            mcbCompany.ValueColumnNo = 0;
            mcbCompany.UserControlToShow = new UclCompany();
            Company _Company = new Company();
            DataTable dtable = _Company.GetOverviewData();
            mcbCompany.FillData(dtable);
        }

        private void FillGenericCategoryCombo()
        {
            mcbGenericCategory.SelectedID = null;
            mcbGenericCategory.SourceDataString = new string[2] { "GenericCategoryId", "GenericCategoryName" };
            mcbGenericCategory.ColumnWidth = new string[2] { "0", "600" };   // kiran
            mcbGenericCategory.ValueColumnNo = 0;
            mcbGenericCategory.UserControlToShow = new UclGenericCategory();
            GenericCategory _GenericCateory = new GenericCategory();
            DataTable dtable = _GenericCateory.GetOverviewData();
            mcbGenericCategory.FillData(dtable);
        }

        private void FillShelfCombo()
        {
            mcbShelfCode.SelectedID = null;
            mcbShelfCode.SourceDataString = new string[2] { "ShelfId", "ShelfCode" };
            mcbShelfCode.ColumnWidth = new string[2] { "0", "200" };
            mcbShelfCode.ValueColumnNo = 0;
            mcbShelfCode.UserControlToShow = new UclShelf();
            Shelf _Shelf = new Shelf();
            DataTable dtable = _Shelf.GetOverviewData();
            mcbShelfCode.FillData(dtable);

        }

        private void FillPack()
        {
            txtPack.SelectedID = null;
            txtPack.SourceDataString = new string[2] { "PackID", "PackName" };
            txtPack.ColumnWidth = new string[2] { "0", "100" };
            Pack _Pack = new Pack();
            DataTable dtable = _Pack.GetOverviewData();
            txtPack.FillData(dtable);
        }
        private void FillPackType()
        {
            txtPackType.SelectedID = null;
            txtPackType.SourceDataString = new string[2] { "ID", "PackTypeName" };
            txtPackType.ColumnWidth = new string[2] { "0", "100" };
            Pack _Pack = new Pack();
            DataTable dtable = _Pack.GetOverviewDataForPackType();
            txtPackType.FillData(dtable);
        }
        private void FillProdCategoryCombo()
        {
            mcbProductCategory.SelectedID = null;
            mcbProductCategory.SourceDataString = new string[3] { "ProductCategoryID", "ProductCategoryName", "SaleDiscount" };
            mcbProductCategory.ColumnWidth = new string[3] { "0", "200", "20" };
            mcbProductCategory.ValueColumnNo = 0;
            mcbProductCategory.UserControlToShow = new UclProdCategory();
            ProductCategory _ProductCategory = new ProductCategory();
            DataTable dtable = _ProductCategory.GetOverviewData();
            mcbProductCategory.FillData(dtable);
        }



        #endregion

        #region Events

        private void mcbCompany_KeyDown(object sender, KeyEventArgs e)
        {
            //label1.Text = "Enter the Company Name of the product";
            switch (e.KeyCode)
            {

                case Keys.Enter:
                    if (mcbCompany.SeletedItem != null)
                    {
                        _Product.ProdCompShortName = mcbCompany.SeletedItem.ItemData[2];
                        txtCompShortName.Text = _Product.ProdCompShortName.ToString();
                    }
                    txtCompShortName.Focus();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    txtCompShortName.Focus();
                    break;
                case Keys.Up:
                    mcbCompany.Focus();
                    break;
            }
        }



        private void txtCompShortName_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit || _Mode == OperationMode.OpenAsChild) && ValidateEmptyorNullData(txtCompShortName.Text.Trim(), "Please Enter Company Short Name.") == true)
                        txtCompShortName.Focus();
                    else txtPack.Focus();
                    break;
                case Keys.Down:
                    txtPack.Focus();
                    break;
                case Keys.Up:
                    mcbCompany.Focus();
                    break;

            }
        }



        //private void txtLoosePack_KeyDown(object sender, KeyEventArgs e)
        //{

        //    //     label1.Text = "You can Make it the Short Company Name of the product";
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Enter:
        //            if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit || _Mode == OperationMode.OpenAsChild) && ValidateEmptyorNullData(txtLoosePack.Text.Trim(), "Please Enter Unit of Measure.") == true)

        //                txtPack.SetFocus();
        //            e.Handled = true;
        //            break;
        //        case Keys.Down:
        //            txtPack.SetFocus();
        //            e.Handled = true;
        //            break;
        //        case Keys.Up:
        //            txtCompShortName.Focus();
        //            e.Handled = true;
        //            break;
        //    }
        //}

        private void txtPack_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtVAT.Focus();
                    break;
                case Keys.Down:
                    txtVAT.Focus();
                    break;
                case Keys.Up:
                    txtPack.Focus();
                    break;
            }
        }

        private void txtVAT_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    txtHSNNumber.Focus();
                    //    txtCST.Focus();
                    break;
                case Keys.Enter:
                    txtHSNNumber.Focus();
                    //   txtCST.Focus();
                    break;
                case Keys.Down:
                    txtHSNNumber.Focus();
                    //   txtCST.Focus();
                    break;
                case Keys.Up:
                    txtPackType.Focus();
                    break;
            }
        }
        private void txtHSNNumber_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtMinLevel.Focus();
                    break;
                case Keys.Down:
                    txtMinLevel.Focus();
                    break;
                case Keys.Up:
                    txtVAT.Focus();
                    break;
            }
        }


        private void txtMinLevel_KeyDown(object sender, KeyEventArgs e)
        {
            label1.Text = "Enter the Maximum level of the product";
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtMaxLevel.Focus();
                    break;
                case Keys.Down:
                    txtMaxLevel.Focus();
                    break;
                case Keys.Up:
                    txtHSNNumber.Focus();
                    //     txtCST.Focus();
                    break;
            }
        }
        private void txtMaxLevel_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtRequireColdStorage.Focus();
                    break;
                case Keys.Down:
                    txtRequireColdStorage.Focus();
                    break;
                case Keys.Up:
                    txtMinLevel.Focus();
                    break;
            }
        }

        //private void txtBoxQty_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Enter:
        //            txtShowInShortList.Focus();
        //            break;
        //        case Keys.Down:
        //            txtShowInShortList.Focus();
        //            break;
        //        case Keys.Up:
        //            mcbSecondCreditor.Focus();

        //            break;
        //        case Keys.Left:
        //            mcbSecondCreditor.Focus();
        //            break;
        //    }
        //}
        private void mcbGeneric_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    mcbProductCategory.Focus();
                    break;
                case Keys.Down:
                    mcbProductCategory.Focus();
                    break;
                case Keys.Up:
                    mcbShelfCode.Focus();
                    break;
            }
        }


        //private void txtLoosePack_Validating(object sender, CancelEventArgs e)
        //{
        //    if (txtLoosePack.Text.Trim() != "")
        //    {
        //        int numberEntered = int.Parse(txtLoosePack.Text.Trim());
        //        if (numberEntered < 1)
        //        {
        //            e.Cancel = true;
        //            MessageBox.Show("Not valid entry", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //}
        private void mcbCompany_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (mcbCompany.SeletedItem != null)//&& _Product.ProdCompShortName.ToString() == "")
                {
                    mcbCompany.SelectedID = mcbCompany.SeletedItem.ItemData[0];
                    _Product.ProdCompShortName = mcbCompany.SeletedItem.ItemData[2];
                    txtCompShortName.Text = _Product.ProdCompShortName.ToString();
                    //if (mcbCompany.SeletedItem.ItemData[3] != null && mcbCompany.SeletedItem.ItemData[3].ToString() != string.Empty)
                    //{
                    //    _Product.ProdCreditor1ID = Convert.ToInt32(mcbCompany.SeletedItem.ItemData[3].ToString());
                    //    mcbFirstCreditor.SelectedIntID  = _Product.ProdCreditor1ID;
                    //    mcbFirstCreditor.Enabled = false;
                    //}

                    //if (mcbCompany.SeletedItem.ItemData[4] != null && mcbCompany.SeletedItem.ItemData[4].ToString() != string.Empty)
                    //{
                    //    _Product.ProdCreditor2ID = Convert.ToInt32 ( mcbCompany.SeletedItem.ItemData[4].ToString());
                    //    mcbSecondCreditor.SelectedIntID = _Product.ProdCreditor2ID;
                    //    mcbSecondCreditor.Enabled = false;
                    //}

                    //FillCreditorList(); // ansuman

                }
                if (Mode == OperationMode.Add || Mode == OperationMode.Edit)
                {
                    if (mcbCompany.SeletedItem != null && ValidateEmptyorNullData(mcbCompany.SeletedItem.Text.Trim(), "Please Enter Company Name.") == true)
                        mcbCompany.Focus();
                    else if (mcbCompany.SeletedItem == null && ValidateEmptyorNullData(string.Empty, "Please Enter Company Name.") == true)
                        mcbCompany.Focus();
                    else txtCompShortName.Focus();
                }
                else
                    txtCompShortName.Focus();
                //if (mcbCompany.SeletedItem != null)
                //{
                //    mcbCompany.SelectedID = mcbCompany.SeletedItem.ItemData[0];
                //    _Product.ProdCompShortName = mcbCompany.SeletedItem.ItemData[2];
                //    txtCompShortName.Text = _Product.ProdCompShortName.ToString();
                //}
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        //private void FillCreditorList()
        //{
        //    DataTable Dt1 = pc.GetPartyByCompID(mcbCompany.SelectedID);
        //    if (Dt1 != null)
        //    {
        //        DataRow[] dr = Dt1.Select("CompID ='" + mcbCompany.SelectedID + "'");
        //        if (dr.Length > 0)
        //            mcbFirstCreditor.SelectedID = dr[0]["AccountID"].ToString();
        //        if (dr.Length > 1)
        //            mcbSecondCreditor.SelectedID = dr[1]["AccountID"].ToString();
        //        if (dr.Length > 2)
        //            mcbThirdCreditor.SelectedID = dr[2]["AccountID"].ToString();
        //        if (dr.Length > 3)
        //            mcbFourthCreditor.SelectedID = dr[3]["AccountID"].ToString();
        //        if (dr.Length == 0)
        //        {
        //            mcbFirstCreditor.SelectedID = "";
        //            mcbSecondCreditor.SelectedID = "";
        //            mcbThirdCreditor.SelectedID = "";
        //            mcbFourthCreditor.SelectedID = "";
        //        }
        //    }
        //}
        private void mcbCompany_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbCompany.SeletedItem != null)
            {
                //_Product.ProdCreditor1ID = Convert.ToInt32(mcbCompany.SeletedItem.ItemData[4]);

            }
        }



        private void mcbCompany_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbCompany.SelectedID;
            if (mcbCompany.SelectedID != null && mcbCompany.SelectedID != "")
            {
                FillCompanyCombo();
                mcbCompany.SelectedID = selectedId;
                _Product.ProdCompShortName = mcbCompany.SeletedItem.ItemData[2];
                txtCompShortName.Text = _Product.ProdCompShortName.ToString();
                _Product.ProdCreditor1ID = Convert.ToInt32(mcbCompany.SeletedItem.ItemData[4]);
                //if (mcbCompany.SeletedItem.ItemData[3] != null && mcbCompany.SeletedItem.ItemData[3].ToString() != string.Empty)
                //{
                //    _Product.ProdCreditor1ID = Convert.ToInt32( mcbCompany.SeletedItem.ItemData[3].ToString());
                //    mcbFirstCreditor.SelectedIntID  = _Product.ProdCreditor1ID;
                //}

                //if (mcbCompany.SeletedItem.ItemData[4] != null && mcbCompany.SeletedItem.ItemData[4].ToString() != string.Empty)
                //{
                //    _Product.ProdCreditor2ID = Convert.ToInt32 ( mcbCompany.SeletedItem.ItemData[4].ToString());
                //    mcbSecondCreditor.SelectedIntID  = _Product.ProdCreditor2ID;
                //}
                txtCompShortName.Focus();
            }

        }

        private void mcbGenericCategory_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbGenericCategory.SelectedID;
            FillGenericCategoryCombo();
            mcbGenericCategory.SelectedID = selectedId;
        }

        private void mcbShelfCode_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbShelfCode.SelectedID;
            FillShelfCombo();
            mcbShelfCode.SelectedID = selectedId;
        }

        private void mcbProductCategory_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = string.Empty;
            if (mcbProductCategory.SelectedID != null)
                selectedId = mcbProductCategory.SelectedID;
            FillProdCategoryCombo();
            mcbProductCategory.SelectedID = selectedId;
            if (mcbProductCategory.SeletedItem != null)
            {
                if (mcbProductCategory.SeletedItem.ItemData[2] != null)
                    _Product.ProdIfSaleDisc = mcbProductCategory.SeletedItem.ItemData[2];
                if (_Product.ProdIfSaleDisc == string.Empty || _Product.ProdIfSaleDisc == " ")
                    _Product.ProdIfSaleDisc = "N";
                //txtSaleDiscount.Text = _Product.ProdIfSaleDisc;
            }
        }

        //private void mcbFirstCreditor_ItemAddedEdited(object sender, EventArgs e)
        //{
        //    string selectedId = mcbFirstCreditor.SelectedID;
        //    FillFirstCreditorCombo();
        //    mcbFirstCreditor.SelectedID = selectedId;
        //}

        //private void mcbSecondCreditor_ItemAddedEdited(object sender, EventArgs e)
        //{
        //    string selectedId = mcbSecondCreditor.SelectedID;
        //    FillSecondCreditorCombo();
        //    mcbSecondCreditor.SelectedID = selectedId;
        //}

        private void txtMaxLevel_KeyUp(object sender, KeyEventArgs e)
        {
            txtMinLevel.Focus();
        }

        private void txtPack_EnterKeyPressed(object sender, EventArgs e)
        {
            int mlen = 0;
            string mtext = "";
            if (txtPack.Text != null && txtPack.Text.ToString() != string.Empty)
            {
                mtext = txtPack.Text.ToString();
                mlen = mtext.Length;
                if (mlen > 6)
                {
                    mtext = txtPack.Text.ToString().Substring(0, 6);
                    txtPack.Text = mtext;
                }

            }
            if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit || _Mode == OperationMode.OpenAsChild) && ValidateEmptyorNullData(txtPack.Text.Trim(), "Please Enter Pack.") == true)
                txtPack.Focus();
            else txtPackType.Focus();

        }

        private void txtVAT_Validating(object sender, CancelEventArgs e)
        {
            double mvatper = 0;
            mvatper = Convert.ToDouble(txtVAT.Text.ToString());
            bool ifvalidvatpercent = _Product.GetVatPercent(mvatper);
            if (!ifvalidvatpercent)
            {
                lblMessage.Text = "Please Check GST Percentage";
                txtVAT.Focus();
            }
            else
                lblMessage.Text = "";
        }

        //private void cbGrade_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        txtProductBarcode.Focus();
        //    else if (e.KeyCode == Keys.Up)
        //        cbSchedule.Focus();
        //}

        //private void mcbScheduleDrug_EnterKeyPressed(object sender, EventArgs e)
        //{
        //    txtShowInShortList.Focus();
        //}

        //private void mcbScheduleDrug_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Left)
        //        txtIfScheduledDrug.Focus();
        //    else if (e.KeyCode == Keys.Down)
        //        txtSaleDiscount.Focus();
        //}


        private void txtRequireColdStorage_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    txtMinLevel.Focus();
                    break;
                case Keys.Enter:
                    mcbShelfCode.Focus();
                    break;
                case Keys.Down:
                    mcbShelfCode.Focus();
                    break;
            }
        }
        private void mcbShelfCode_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbShelfCode.SelectedID == null)
                FillShelfCombo();

            mcbGenericCategory.Focus();
        }
        private void mcbGenericCategory_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbProductCategory.Focus();
        }
        private void mcbProductCategory_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbProductCategory.SeletedItem != null)
            {
                if (mcbProductCategory.SeletedItem.ItemData[2] != null)
                {
                    _Product.ProdIfSaleDisc = mcbProductCategory.SeletedItem.ItemData[2];
                    if (_Product.ProdIfSaleDisc == string.Empty || _Product.ProdIfSaleDisc == " ")
                        _Product.ProdIfSaleDisc = "N";
                }

            }
            //txtSaleDiscount.Text = _Product.ProdIfSaleDisc;
            if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit || _Mode == OperationMode.OpenAsChild) && mcbProductCategory.SeletedItem != null && ValidateEmptyorNullData(mcbProductCategory.SeletedItem.Text.Trim(), "Please Enter Product Category.") == true)
                mcbProductCategory.Focus();
            else if (mcbProductCategory.SeletedItem == null && ValidateEmptyorNullData(string.Empty, "Please Enter Product Category.") == true)
                mcbProductCategory.Focus();
            else txtBoxQty.Focus();

        }

        //private void mcbFirstCreditor_EnterKeyPressed(object sender, EventArgs e)
        //{
        //    mcbSecondCreditor.Focus();
        //}

        //private void mcbSecondCreditor_EnterKeyPressed(object sender, EventArgs e)
        //{
        //    mcbThirdCreditor.Focus();
        //    //txtBoxQty.Focus();
        //}



        #endregion

        # region tooltip

        private void AddToolTip()
        {
            ttproduct.SetToolTip(txtCompShortName, "Maximum 3 Letters");
            //    ttproduct.SetToolTip(txtCST, "Central Tax");
            //ttproduct.SetToolTip(txtLoosePack, "Quantity per pack if sold in loose quantity and NOT by pack. Enter 1 if sold by pack");
            ttproduct.SetToolTip(txtName, "A-Z,0-9,space only");
            ttproduct.SetToolTip(txtPack, "TAB,CAP,NO,VIAL etc Max Characters 6 (Six)");
            //ttproduct.SetToolTip(cbGrade, "Grade according to Sale or Rate or Availability ");
            ttproduct.SetToolTip(txtVAT, "GST % = 0.00 5.0, 12.00, 18.00,28.00");
            //   ttproduct.SetToolTip(txtCST, "CST in Rupees");
            mcbProductCategory.SetToolTip(ttproduct, "Fill Product Category");
            mcbProductCategory.SetToolTipNewButton(ttproduct, "Click to create new Product Category");
            ttproduct.SetToolTip(mcbGenericCategory, "Fill Generic Category");
            ttproduct.SetToolTip(txtPack, "Fill Packing");
            ttproduct.SetToolTip(txtMaxLevel, "Minimum Level >= 0");
            ttproduct.SetToolTip(txtMaxLevel, "Maximum Level >= Minimum Level");

        }
        #endregion

        #region UIEvents
        private void UclProduct_Load(object sender, EventArgs e)
        {
            SetFocus();
        }
        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            //kiran
            bool SetBtnFocus = false;
            if (txtName.SelectedID != null && txtName.SelectedID != string.Empty && Prod_SelectedID != txtName.SelectedID)
            {
                FillSearchData(txtName.SelectedID, "");
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    DialogResult result = MessageBox.Show("Are you trying make it duplicate?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Prod_SelectedID = txtName.SelectedID;
                        SetBtnFocus = true;
                        txtName.Focus();
                    }
                }
            }
            _Product.Name = txtName.Text;
            if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit || _Mode == OperationMode.OpenAsChild) && ValidateEmptyorNullData(txtName.Text.Trim(), "Please Enter Product Name.") == true)
                txtName.Focus();
            else if (SetBtnFocus == false)
                mcbCompany.Focus();
        }
        //private void chkIfScheduledDrug_KeyDown(object sender, KeyEventArgs e)
        //{
        //    cbSchedule.Focus();
        //}
        private void mcbGenericCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                txtBoxQty.Focus();
        }

        //private void txtIfScheduledDrug_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Up:
        //            txtShowInShortList.Focus();
        //            break;
        //        case Keys.Down:
        //            txtSaleDiscount.Focus();
        //            break;
        //        case Keys.Enter:
        //            txtSaleDiscount.Focus();
        //            break;
        //    }
        //}
        //private void cbSchedule_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Up:
        //            txtSaleDiscount.Focus();
        //            break;
        //        case Keys.Enter:
        //            cbGrade.Focus();
        //            cbGrade.DroppedDown = true;
        //            break;
        //    }
        //}
        //private void txtShowInShortList_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Enter:
        //            txtIfScheduledDrug.Focus();
        //            break;
        //        case Keys.Down:
        //            txtIfScheduledDrug.Focus();
        //            break;
        //        case Keys.Up:
        //            txtBoxQty.Focus();
        //            break;
        //    }
        //}

        //private void txtSaleDiscount_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Up:
        //            txtIfScheduledDrug.Focus();
        //            break;
        //        case Keys.Enter:
        //            cbSchedule.Focus();
        //            cbSchedule.DroppedDown = true;
        //            break;
        //        case Keys.Down:
        //            cbSchedule.Focus();
        //            cbSchedule.DroppedDown = true;
        //            break;
        //    }
        //}
        //private void txtIfBarCodeRequired_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Up:
        //            txtProductBarcode.Focus();
        //            break;
        //        case Keys.Enter:
        //            MainToolStrip.Select();
        //            tsBtnSave.Select();
        //            break;
        //            //case Keys.Down:
        //            //    txtAddOctroi.Focus();
        //            //    break;
        //    }
        //}
        //private void txtAddOctroi_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Up)
        //        txtIfBarCodeRequired.Focus();
        //}

        private void mcbShelfCode_UpArrowPressed(object sender, EventArgs e)
        {
            txtRequireColdStorage.Focus();
        }
        //private void txtPack_UpArrowKeyPressed(object sender, EventArgs e)
        //{
        //    txtLoosePack.Focus();
        //}

        private void mcbGenericCategory_UpArrowPressed(object sender, EventArgs e)
        {
            mcbShelfCode.Focus();
        }
        private void mcbProductCategory_UpArrowPressed(object sender, EventArgs e)
        {
            mcbGenericCategory.Focus();
        }
        private void mcbFirstCreditor_UpArrowPressed(object sender, EventArgs e)
        {
            mcbProductCategory.Focus();
        }
        //private void mcbSecondCreditor_UpArrowPressed(object sender, EventArgs e)
        //{
        //    mcbFirstCreditor.Focus();
        //}

        private void txtPackType_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtPack.Focus();
        }
        private void txtPackType_EnterKeyPressed(object sender, EventArgs e)
        {
            if ((Mode == OperationMode.Add || Mode == OperationMode.Edit || Mode == OperationMode.OpenAsChild) && ValidateEmptyorNullData(txtPackType.Text.Trim(), "Please Enter Packing Type.") == true)
                txtPackType.Focus();
            else
                txtVAT.Focus();
        }
        private void mcbCompany_UpArrowPressed(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        //private void txtIfBarCodeRequired_TextChanged(object sender, EventArgs e)
        //{
        //    txtProductBarcode.Enabled = txtIfBarCodeRequired.Text.Equals("N");
        //}

        private void txtPackType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                txtPack.Focus();
        }
        //private void txtProductBarcode_KeyDown(object sender, KeyEventArgs e) // [09.02.2017]
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (txtProductBarcode.Text == null || txtProductBarcode.Text.ToString() == string.Empty || txtProductBarcode.Text.ToString() == "")
        //        {
        //            txtIfBarCodeRequired.Enabled = true;
        //            txtIfBarCodeRequired.Focus();
        //        }
        //        else
        //        {
        //            txtIfBarCodeRequired.Text = "N";
        //            txtIfBarCodeRequired.Enabled = false;
        //            DataRow dr = _Product.GetDuplicateBarcode(txtProductBarcode.Text);
        //            if (dr != null)
        //            {
        //                PSMessageBox.Show("Duplicate Barcode Found", "Invalid Barcode", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
        //                txtProductBarcode.Focus();
        //                return;
        //            }
        //        }
        //    }
        //    else if (e.KeyCode == Keys.Up)
        //    {
        //        cbGrade.Focus();
        //        cbGrade.DroppedDown = true;
        //    }
        //}
        //private void mcbThirdCreditor_ItemAddedEdited(object sender, EventArgs e)
        //{
        //    string selectedId = mcbThirdCreditor.SelectedID;
        //    FillThirdCreditorCombo();
        //    mcbThirdCreditor.SelectedID = selectedId;
        //}

        //private void mcbFourthCreditor_ItemAddedEdited(object sender, EventArgs e)
        //{
        //    string selectedId = mcbFourthCreditor.SelectedID;
        //    FillFourthCreditorCombo();
        //    mcbFourthCreditor.SelectedID = selectedId;
        //}

        //private void mcbThirdCreditor_EnterKeyPressed(object sender, EventArgs e)
        //{
        //    mcbFourthCreditor.Focus();
        //}

        private void mcbFourthCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtBoxQty.Focus();
        }

        #endregion UIEvents

        #region ValidateData

        private bool ValidateEmptyorNullData(object Data, string Msg)
        {
            bool result = false;
            if (string.IsNullOrEmpty(Convert.ToString(Data)) == true)
            {
                MessageBox.Show(Msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = true;
            }
            return result;
        }




        #endregion ValidateData


    }
}
