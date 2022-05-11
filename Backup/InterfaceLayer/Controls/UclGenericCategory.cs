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

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclGenericCategory : BaseControl
    {
        #region Declaration
        private GenericCategory _GenericCategory;
        #endregion

        #region Constructors
        public UclGenericCategory()
        {
            try
            {
                InitializeComponent();
                _GenericCategory = new GenericCategory();
                SearchControl = new UclGenericCategorySearch();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Constructor      

        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _GenericCategory.Initialise();
                ClearControls();
                txtName.Focus();
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
                retValue = base.Add();
                ClearData();
                pnlCenter.Enabled = true;
                FilltxtName();
                AddToolTip();
                headerLabel1.Text = "GENERIC CATEGORY -> NEW";
                txtName.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = false;
            try
            {
                retValue = base.Edit();
                ClearData();
                pnlCenter.Enabled = true;
                AddToolTip();
                headerLabel1.Text = "GENERIC CATEGORY -> EDIT";
                FilltxtName();
                txtName.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = false;
            try
            {
                retValue = base.Cancel();
                pnlCenter.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = false;
            try
            {
                retValue = base.Delete();
                headerLabel1.Text = "GENERIC CATEGORY -> DELETE";
                ClearData();
                FilltxtName();
                txtName.Focus();
                pnlCenter.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            try
            {
                if (_GenericCategory.Id != null && _GenericCategory.Id != "")
                {
                    retValue = _GenericCategory.CanBeDeleted();
                    if (retValue == true)
                    {
                        retValue = _GenericCategory.DeleteDetails();
                        MessageBox.Show("GenericCategory information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                pnlCenter.Enabled = true;
                ClearData();
                FilltxtName();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        public override bool View()
        {
            bool retValue = false;
            try
            {
                retValue = base.View();
                ClearData();
                FilltxtName();
                headerLabel1.Text = "GENERIC CATEGORY -> VIEW";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                _GenericCategory.Name = txtName.Text;
                if (_Mode == OperationMode.Edit)
                    _GenericCategory.IFEdit = "Y";
                _GenericCategory.Validate();
                if (_GenericCategory.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _GenericCategory.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _GenericCategory.CreatedBy = General.CurrentUser.Id;
                        _GenericCategory.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _GenericCategory.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _GenericCategory.AddDetails();
                        MessageBox.Show("GenericCategory information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _GenericCategory.Id;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _GenericCategory.ModifiedBy = General.CurrentUser.Id;
                        _GenericCategory.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _GenericCategory.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _GenericCategory.UpdateDetails();
                        MessageBox.Show("GenericCategory information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _GenericCategory.Id;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }
                }
                else // Show Validation Messages
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _GenericCategory.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _GenericCategory.Id = ID;
                    _GenericCategory.ReadDetailsByID();
                    txtName.Text = _GenericCategory.Name;
                    txtName.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        public void FilltxtName()
        {
            try
            {
                txtName.SelectedID = null;
                txtName.SourceDataString = new string[2] { "GenericCategoryID", "GenericCategoryName" };
                txtName.ColumnWidth = new string[2] { "0", "300" };
                GenericCategory _txt = new GenericCategory();
                DataTable dtable = _txt.GetOverviewData();
                txtName.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
            try
            {
                if (_GenericCategory.Name != null && _GenericCategory.Name != txtName.Text)
                    retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        private void ClearControls()
        {
            try
            {
                txtName.Text = "";
                txtName.SelectedID = "";
                txtName.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Events

        private void txtName_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtName.SelectedID != null && txtName.SelectedID != "")
                {
                    _GenericCategory.Id = txtName.SelectedID;
                    FillSearchData(txtName.SelectedID,"");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttGeneric.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion
    }
}
