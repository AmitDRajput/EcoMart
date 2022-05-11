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
    public partial class UclMessages : BaseControl 
    {
        #region Declaration     
        private Messages  _Message;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        #endregion

        #region Constructor
        public UclMessages()
        {
            try
            {
                InitializeComponent();
                _Message = new Messages();
                SearchControl = new UclMessagesSearch();
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
                _Message.Initialise();
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
                headerLabel1.Text = "MESSAGES -> NEW";
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
                headerLabel1.Text = "MESSAGES -> EDIT";
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
                headerLabel1.Text = "MESSAGES -> DELETE";
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
                if (_Message.Id != null && _Message.Id != "")
                {
                    retValue = _Message.CanBeDeleted();
                    if (retValue == true)
                    {
                        retValue = _Message.DeleteDetails();
                        MessageBox.Show("Message information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("MessageID", "Message", "Mastermessage");
            bool retValue = false;
            try
            {
                retValue = base.View();
                ClearData();
                FilltxtName();
                headerLabel1.Text = "MESSAGES -> VIEW";
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
                _Message.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Message.Id, "");

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
                    _Message.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Message.Id, "");
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
                _Message.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Message.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Message.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Message.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                _Message.Name = txtName.Text;
                if (_Mode == OperationMode.Edit)
                    _Message.IFEdit = "Y";
                _Message.Validate();
                if (_Message.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                       /* _Message.IntID = _Message.GetIntID(); Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/
                        _Message.CreatedBy = General.CurrentUser.Id;
                        _Message.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Message.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        //retValue = _Message.AddDetails();
                        //MessageBox.Show("Message information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //_SavedID = _Message.Id;
                        //ClearControls();
                        //FilltxtName();
                        //retValue = true;
                        _Message.IntID = _Message.AddDetails();
                        if (_Message.IntID > 0)
                        {
                            MessageBox.Show("Email information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SaveIntID = _Message.IntID;
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
                        _Message.ModifiedBy = General.CurrentUser.Id;
                        _Message.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Message.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Message.UpdateDetails();
                        MessageBox.Show("Message information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Message.Id;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }
                }
                else // Show Validation Messages
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Message.ValidationMessages)
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
                    _Message.Id = ID;
                    _Message.ReadDetailsByID();
                    txtName.Text = _Message.Name;
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
                txtName.SourceDataString = new string[2] { "ID", "Message" };
                txtName.ColumnWidth = new string[2] { "0", "300" };
                Messages _txt = new Messages();
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
                if (_Message.Name != null && _Message.Name != txtName.Text)
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
                    _Message.Id = txtName.SelectedID;
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
            ttmessage.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion     
    }
}
