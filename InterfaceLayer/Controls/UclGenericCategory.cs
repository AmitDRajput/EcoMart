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
using System.Collections;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclGenericCategory : BaseControl
    {
        #region Declaration
        private GenericCategory _GenericCategory;
        Hashtable htTableList;
        public int CurrentNumber = 0;
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
            htTableList = General.GetTableListByCode("GenericCategoryID", "GenericCategoryName", "MasterGenericCategory");
            bool retValue = false;
            try
            {
                retValue = base.View();
                ClearData();
                FilltxtName();
                headerLabel1.Text = "GENERIC CATEGORY -> VIEW";
                MoveLast();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _GenericCategory.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_GenericCategory.Id, "");

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
                    _GenericCategory.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_GenericCategory.Id, "");
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
                _GenericCategory.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_GenericCategory.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _GenericCategory.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_GenericCategory.Id, "");
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
                        _GenericCategory.IntID  = _GenericCategory.GetIntID();
                        _GenericCategory.CreatedBy = General.CurrentUser.Id;
                        _GenericCategory.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _GenericCategory.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _GenericCategory.IntID = _GenericCategory.GetNextIntID();
                        if (_GenericCategory.IntID > 0)
                            retValue = _GenericCategory.AddDetails();
                        if (retValue)
                        {
                            MessageBox.Show("GenericCategory  information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SaveIntID = _GenericCategory.IntID;
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
                        _GenericCategory.ModifiedBy = General.CurrentUser.Id;
                        _GenericCategory.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _GenericCategory.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        _GenericCategory.Id = txtName.SelectedID.ToString();
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
                txtName.ColumnWidth = new string[2] { "0", "450" };
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
        public override void ReFillData(Control closedControl)
        {
            // FilltxtName();
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
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
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
                    FillSearchData(txtName.SelectedID, "");
                    if (txtName.SelectedID != null && txtName.SelectedID.ToString() != string.Empty)
                        lblMessageText.Text = "Please Press Tab After Modifications Done...";
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

        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtName.Text)) == true)
                txtName.Focus();
            else
                txtIfScheduledDrug.Focus();
        }

        private void txtIfScheduledDrug_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
            else if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                txtName.Focus();
            }
        }
    }
}