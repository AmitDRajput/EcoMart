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
    public partial class UclEmailID : BaseControl
    {
        #region Declaration
        private EmailID _EmailID;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        #endregion

        #region Constructor
        public UclEmailID()
        {
            try
            {
                InitializeComponent();
                _EmailID = new EmailID();
                SearchControl = new UclEmailIDSearch();
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
                _EmailID.Initialise();
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
                headerLabel1.Text = "EmailID -> NEW";
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
                headerLabel1.Text = "EmailID -> EDIT";
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
                headerLabel1.Text = "EmailID -> DELETE";
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
                _EmailID.Id = txtName.SelectedID;
                if (_EmailID.Id != null && _EmailID.Id != "")
                {
                    retValue = _EmailID.CanBeDeleted();
                    if (retValue == true)
                    {
                        retValue = _EmailID.DeleteDetails();
                        MessageBox.Show("Information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("emailID","EmailName", "masterEmail");
            bool retValue = false;
            try
            {
                retValue = base.View();
                ClearData();
                FilltxtName();
                headerLabel1.Text = "EmailID -> VIEW";
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
                _EmailID.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_EmailID.Id, "");

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
                    _EmailID.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_EmailID.Id, "");
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
                _EmailID.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_EmailID.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _EmailID.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_EmailID.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                _EmailID.Name = txtName.Text.ToString();
                _EmailID.Details = txtDetails.Text.ToString();
                if (_Mode == OperationMode.Edit)
                    _EmailID.IFEdit = "Y";
                _EmailID.Validate();
                if (_EmailID.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _EmailID.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _EmailID.CreatedBy = General.CurrentUser.Id;
                        _EmailID.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _EmailID.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        //retValue = _EmailID.AddDetails();
                        //MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //_SavedID = _EmailID.Id;
                        //ClearControls();
                        //FilltxtName();
                        _EmailID.IntID = _EmailID.AddDetails();
                        if (_EmailID.IntID > 0)
                        {
                            MessageBox.Show("Email information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SaveIntID = _EmailID.IntID;
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
                        _EmailID.ModifiedBy = General.CurrentUser.Id;
                        _EmailID.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _EmailID.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _EmailID.UpdateDetails();
                        MessageBox.Show("Information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _EmailID.Id;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }
                }
                else // Show Validation Messages
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _EmailID.ValidationMessages)
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
                    _EmailID.Id = ID;
                    _EmailID.ReadDetailsByID();
                    txtName.Text = _EmailID.Name;
                    txtDetails.Text = _EmailID.Details;
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
                txtName.SourceDataString = new string[2] { "EmailID", "EmailName" };
                txtName.ColumnWidth = new string[2] { "0", "300" };
                EmailID _txt = new EmailID();
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
            try
            {
                if (_EmailID.Name != null && _EmailID.Name != txtName.Text)
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
                txtDetails.Text = "";
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
            txtDetails.Focus();
        }       


        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttToolTip.SetToolTip(txtName, "Valid EmailID");
        }
        #endregion

        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            txtDetails.Focus();
        }
       
    }
}
