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
    public partial class UclCustomer : BaseControl
    {
        #region Declaration     
        private Customer _Customer;      
        #endregion

        #region Constructor
        public UclCustomer()
        {
            try
            {
                InitializeComponent();
                _Customer = new Customer();
                SearchControl = new UclCustomerSearch();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }       
        #endregion       

        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _Customer.Initialise();
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
                headerLabel1.Text = "Customer -> NEW";
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
                headerLabel1.Text = "Customer -> EDIT";
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
                headerLabel1.Text = "Customer -> DELETE";
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
                _Customer.Id = txtName.SelectedID;
                if (_Customer.Id != null && _Customer.Id != "")
                {
                    retValue = _Customer.CanBeDeleted();
                    if (retValue == true)
                    {
                        retValue = _Customer.DeleteDetails();
                        MessageBox.Show("Customer information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                headerLabel1.Text = "Customer -> VIEW";
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
                _Customer.Name = txtName.Text;
                if (_Mode == OperationMode.Edit)
                    _Customer.IFEdit = "Y";
                _Customer.Validate();
                if (_Customer.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _Customer.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Customer.CreatedBy = General.CurrentUser.Id;
                        _Customer.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Customer.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Customer.AddDetails();
                        MessageBox.Show("Customer information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Customer.Id;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _Customer.ModifiedBy = General.CurrentUser.Id;
                        _Customer.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Customer.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Customer.UpdateDetails();
                        MessageBox.Show("Customer information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Customer.Id;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }
                }
                else // Show Validation Messages
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Customer.ValidationMessages)
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
                    _Customer.Id = ID;
                    _Customer.ReadDetailsByID();
                    txtName.Text = _Customer.Name;
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
                txtName.SourceDataString = new string[2] { "CustomerID", "CustomerNameAddress" };
                txtName.ColumnWidth = new string[2] { "0", "300" };
                Customer _txt = new Customer();
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
                if (_Customer.Name != null && _Customer.Name != txtName.Text)
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
                    _Customer.Id = txtName.SelectedID;
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
            ttCustomer.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion     
    }
}