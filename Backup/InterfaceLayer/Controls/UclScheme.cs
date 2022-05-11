using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.Common;
using System.Text.RegularExpressions;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclScheme : BaseControl
    {
        #region Declaration      
        private Scheme _Scheme;      
        #endregion

        #region  Constructor
        public UclScheme()
        {
            InitializeComponent();
            _Scheme = new Scheme();
            SearchControl = new UclSchemeSearch();
        } 
        #endregion Constructor
                
        # region IDetail Control
        public override void SetFocus()
        {
            mcbProduct.Focus();
        }
        public override bool ClearData()
        {
            _Scheme.Initialise();
            ClearControls();
            mcbProduct.Focus();
            return true;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            panel1.Enabled = true;
            FillProductCombo();
            headerLabel1.Text = "SCHEME -> NEW";
            mcbProduct.Focus();
         //   AddToolTip();
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            headerLabel1.Text = "SCHEME -> EDIT";
        //    AddToolTip();
            FillProductCombo();
            mcbProduct.Focus();
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
            headerLabel1.Text = "SCHEME -> DELETE";
            ClearData();
            FillProductCombo();
            mcbProduct.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;           
            if (_Scheme.Id != null && _Scheme.Id != "")
            {
                retValue = _Scheme.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Scheme.DeleteDetails();
                    MessageBox.Show("Scheme information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            panel1.Enabled = true;
            ClearData();
            FillProductCombo();
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            panel1.Enabled = true;
            ClearData();
            FillProductCombo();
            headerLabel1.Text = "SCHEME -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _Scheme.ProductID = mcbProduct.SelectedID;
            _Scheme.StartDate = datePickerStartDate.Value.Date.ToString("yyyyMMdd");
            _Scheme.ClosingDate = datePickerClosingDate.Value.Date.ToString("yyyyMMdd");
            if (txtQuantity1.Text != null && txtQuantity1.Text != "")
                _Scheme.Quantity1 = Convert.ToInt32(txtQuantity1.Text.ToString());
            if (txtQuantity2.Text != null && txtQuantity2.Text != "")
                _Scheme.Quantity2 = Convert.ToInt32(txtQuantity2.Text.ToString());
            if (txtQuantity3.Text != null && txtQuantity3.Text != "")
                _Scheme.Quantity3 = Convert.ToInt32(txtQuantity3.Text.ToString());
            if (txtScheme1.Text != null && txtScheme1.Text != "")
                _Scheme.Scheme1 = Convert.ToInt32(txtScheme1.Text.ToString());
            if (txtScheme2.Text != null && txtScheme2.Text != "")
                _Scheme.Scheme2 = Convert.ToInt32(txtScheme2.Text.ToString());
            if (txtScheme3.Text != null && txtScheme3.Text != "")
                _Scheme.Scheme3 = Convert.ToInt32(txtScheme3.Text.ToString());
            if (_Scheme.IfSchemeAlreadyExist == "Y")
            {
                _Scheme.IFEdit = "Y";
                _Mode = OperationMode.Edit;
            }
            _Scheme.Validate();
            if (_Scheme.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _Scheme.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _Scheme.CreatedBy = General.CurrentUser.Id;
                    _Scheme.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Scheme.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Scheme.AddDetails();
                    MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Scheme.Id;
                    ClearControls();
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _Scheme.ModifiedBy = General.CurrentUser.Id;
                    _Scheme.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Scheme.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Scheme.UpdateDetails();
                    MessageBox.Show("Information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Scheme.Id;
                //    mcbProduct.Enabled = true;
                    ClearControls();
                    retValue = true;
                }
            }
            else // Show Validation Messages
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Scheme.ValidationMessages)
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
                _Scheme.Id = ID;
                _Scheme.ReadDetailsByID();
          //      FillProductCombo();
                datePickerClosingDate.Enabled = true;
                datePickerStartDate.Enabled = true;
                datePickerStartDate.Value = General.ConvertStringToDateyyyyMMdd(_Scheme.StartDate);
                datePickerClosingDate.Value = General.ConvertStringToDateyyyyMMdd(_Scheme.ClosingDate);
                txtQuantity1.Text = _Scheme.Quantity1.ToString("#0");
                txtQuantity2.Text = _Scheme.Quantity2.ToString("#0");
                txtQuantity3.Text = _Scheme.Quantity3.ToString("#0");
                txtScheme1.Text = _Scheme.Scheme1.ToString("#0");
                txtScheme2.Text = _Scheme.Scheme2.ToString("#0");
                txtScheme3.Text = _Scheme.Scheme3.ToString("#0");
                mcbProduct.SelectedID = _Scheme.ProductID;
                txtPack.Text = _Scheme.Pack.ToString();
                txtUOM.Text = _Scheme.UOM.ToString();
                txtcompanyShortName.Text = _Scheme.CompanyShortName;
               // txtPack.Text = "" ;
                if (txtQuantity1.Text != null && txtQuantity1.Text != "")
                {
                    _Scheme.IfSchemeAlreadyExist = "Y";
                    mcbProduct.Enabled = false;
                }
                
            }
            if (Mode == OperationMode.Delete || Mode == OperationMode.View || Mode == OperationMode.ReportView)
                panel1.Enabled = false;
            return true;
        }

        private void FillProductCombo()
        {
            mcbProduct.SelectedID = null;
            mcbProduct.SourceDataString = new string[5] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName" };
            mcbProduct.ColumnWidth = new string[5] { "0", "200", "30", "30", "30" };
            mcbProduct.ValueColumnNo = 0;
            DataTable dtable = General.ProductList;
            mcbProduct.FillData(dtable);
        }
        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData()
        {
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
         #endregion Idetail Members        

        #region Other private methods

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        private void ClearControls()
        {
            mcbProduct.Enabled = true;
            mcbProduct.SelectedID = "";           
            datePickerStartDate.ResetText();
            datePickerClosingDate.ResetText();
            txtQuantity1.Text = "";
            txtQuantity2.Text = "";
            txtQuantity3.Text = "";
            txtScheme1.Text = "";
            txtScheme2.Text = "";
            txtScheme3.Text = "";
            txtPack.Text = "";
            txtUOM.Text = "";
            txtcompanyShortName.Text = "";
        }
        #endregion OtherPrivate Methods

        #region Events
        //private void mcbProduct_SeletectIndexChanged(object sender, EventArgs e)
        //{
        //    EnterKeyPressed();
        //}

        private void EnterKeyPressed()
        {
            if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
            {
                _Scheme.ProductID = mcbProduct.SelectedID;
                _Scheme.ReadDetailsByProductID();
                
                FillSearchData(_Scheme.Id,"");
                txtUOM.Text = mcbProduct.SeletedItem.ItemData[2];
                txtPack.Text = mcbProduct.SeletedItem.ItemData[3];
                txtcompanyShortName.Text = mcbProduct.SeletedItem.ItemData[4];
                datePickerStartDate.Focus();
            }
        }

        private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        {
            EnterKeyPressed();
        }
        private void datePickerStartDate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    datePickerClosingDate.Focus();
                    break;
                case Keys.Up:
                    mcbProduct.Focus();
                    break;
            }
        }
        private void datePickerClosingDate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtQuantity1.Focus();
                    break;
                case Keys.Up:
                    datePickerStartDate.Focus();
                    break;
            }
        }

        private void txtQuantity1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtScheme1.Focus();
                    break;
                case Keys.Up:
                    datePickerClosingDate.Focus();
                    break;
            }
        }

        private void txtScheme1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtQuantity2.Focus();
                    break;
                case Keys.Up:
                    txtQuantity1.Focus();
                    break;
            }
        }

        private void txtQuantity2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtScheme2.Focus();
                    break;
                case Keys.Up:
                    txtScheme1.Focus();
                    break;
            }

        }

        private void txtScheme2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtQuantity3.Focus();
                    break;
                case Keys.Up:
                    txtQuantity2.Focus();
                    break;
            }
        }

        private void txtQuantity3_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtScheme3.Focus();
                    break;
                case Keys.Up:
                    txtScheme2.Focus();
                    break;
            }
        }

        private void txtScheme3_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    txtQuantity3.Focus();
                    break;
            }
        }

        private void txtScheme1_Validating(object sender, CancelEventArgs e)
        {
            if ( txtScheme1.Text != null &&  txtScheme1.Text != "" &&  Convert.ToInt32(txtScheme1.Text.ToString()) < 0)
            {
                txtScheme1.Text = "";
                txtScheme1.Focus();
            }

        }
        #endregion Events

    }
}



