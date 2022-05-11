using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.InterfaceLayer;
using EcoMart.Common;
using System.Collections;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclUser : BaseControl 
    {
        # region declaration
        private User _User;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        #endregion

        #region  Constructor
        public UclUser()
        {
            try
            {
                InitializeComponent();
                _User = new User();
                SearchControl = new UclAddUserSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
        }
        #endregion Constructor

        #region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _User.Initialise();
                ClearControls();
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            try
            {
                ClearData();
                FillLevelCombo();
                FillUserName();
                chkifuse.Checked = true;
                headerLabel1.Text = "USERS -> NEW";
                txtName.Focus();
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
                //   AddToolTip();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
               // ClearData();
                headerLabel1.Text = "USERS -> EDIT";
                //    AddToolTip();
                FillLevelCombo();
                FillUserName();
                chkifuse.Checked = true;
                tsBtnSavenPrint.Visible = false;
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return retValue;

        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();         
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "USERS -> DELETE";
                ClearData();
                FillLevelCombo();
                FillUserName();
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return true;
        }
        public override bool ProcessDelete()
        {

            try
            {
                if (_User.Id != null && _User.Id != "")
                {
                    if (_User.CanBeDeleted())
                    {
                        _User.IfInUse = 0;
                        _User.DeleteDetails();
                        MessageBox.Show("User information has been deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                ClearData();
                FillLevelCombo();
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return true;
        }

        public override bool View()
        {
            htTableList = General.GetTableListByCode("UserID", "UserName", "tbluser");
            bool retValue = base.View();
            try
            {
                ClearData();
                FillLevelCombo();
                FillUserName();
                txtName.Focus();
                headerLabel1.Text = "USER -> VIEW";
                   MoveLast();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _User.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_User.Id, "");
          
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
                    _User.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_User.Id, "");
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
                _User.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_User.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _User.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_User.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                _User.Name = txtName.Text.Trim();
                _User.Password = txtpasswd.Text.Trim();
                _User.Description = txtdetails.Text.Trim();
                int level;
                int.TryParse(mcblevel.SelectedID, out level);
                _User.Level = level;
                if (_Mode == OperationMode.Edit)
                    _User.IFEdit = "Y";
                else
                    _User.IFEdit = "N";
                if (cbMakeItDefault.Checked == true)
                    _User.MakeItDefault = "Y";
                else
                    _User.MakeItDefault = "N";
                _User.Validate();
                if (_User.IsValid)
                {
                    if (_Mode == OperationMode.Add)
                    {
                        _User.CreatedBy = General.CurrentUser.Id;
                        _User.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _User.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _User.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");

                        if (_User.MakeItDefault == "Y")
                            _User.MakeAllMakeItDefaultNo();
                        retValue = _User.AddDetails();
                       
                        MessageBox.Show("User information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _User.Id;                       
                        retValue = true;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _User.ModifiedBy = General.CurrentUser.Id;
                        _User.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _User.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        if (_User.MakeItDefault == "Y")
                            _User.MakeAllMakeItDefaultNo();
                        retValue = _User.UpdateDetails();
                        MessageBox.Show("User information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _User.Id;                       
                        retValue = true;
                    }
                    ClearData();
                }
                else // Show Validation Messages
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _User.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _User.Id = ID;
                    _User.ReadDetailsByID();
                    if (_User.Level > 0)
                    {
                        txtName.Text = _User.Name;
                        txtpasswd.Text = _User.Password;
                        txtrtyppass.Text = _User.Password;
                        txtdetails.Text = _User.Description;
                        if (_User.MakeItDefault == "Y")
                            cbMakeItDefault.Checked = true;
                        else
                            cbMakeItDefault.Checked = false;
                        if (_User.IfInUse == 1)
                            chkifuse.Checked = true;
                        else
                            chkifuse.Checked = false;
                        FillLevelCombo();
                        mcblevel.SelectedID = _User.Level.ToString();

                        txtName.Enabled = false;
                        txtpasswd.Focus();

                        _User.ModifiedDate = "" + DateTime.Today.ToString("yyyyMMdd");
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return true;
        }

        #endregion IDetail Control

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            if (keyPressed == Keys.N && modifier == Keys.Alt)
            {
                txtName.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.P  && modifier == Keys.Alt)
            {
                txtpasswd.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.R  && modifier == Keys.Alt)
            {
                txtrtyppass.Focus();
                retValue = true;
            }         
            if (keyPressed == Keys.D && modifier == Keys.Alt)
            {
                txtdetails.Focus();
                retValue = true;
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }
  
       
        public void ClearControls()
        {
            try
            {
                txtName.Text = "";
                txtName.Enabled = true;
                txtpasswd.Clear();
                txtrtyppass.Clear();
                txtdetails.Clear();
                mcblevel.SelectedID = "";
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
        }
      

        #region other Methods
        private void FillLevelCombo()
        {
            try
            {
                mcblevel.SelectedID = null;
                mcblevel.SourceDataString = new string[2] { "ID", "Type" };
                mcblevel.ColumnWidth = new string[2] { "0", "200" };
                mcblevel.ValueColumnNo = 0;
                //     mcblevel.UserControlToShow = new UclUser(true);
                User _user = new User();
                DataTable dtable = _user.GetLevelData();
                mcblevel.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
        }

        private void FillUserName()
        {
            try
            {
                txtName.SelectedID = null;
                txtName.SourceDataString = new string[2] { "UserID", "UserName" };
                txtName.ColumnWidth = new string[2] { "0", "200" };
                txtName.ValueColumnNo = 0;
                User _user = new User();
                DataTable dt = _user.GetOverviewData();
                txtName.FillData(dt);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          

        }
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        public bool CheckPassword()
        {
            bool retvalue = false;
            try
            {
                _User.Password = txtpasswd.Text.Trim();
                _User.RetypePassword = txtrtyppass.Text.Trim();
                if (_User.Password == _User.RetypePassword)
                {
                    retvalue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return retvalue;
        }
        #endregion

        #region Events
        private void txtusrname_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtpasswd.Focus();
                    break;
                case Keys.Down:
                    txtpasswd.Focus();
                    break;
                case Keys.Up:
                    //txtpasswd.Focus();
                    break;
            }

        }

        private void txtpasswd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _User.Password = txtpasswd.Text;
                    txtrtyppass.Focus();
                    break;
                case Keys.Down:
                    _User.Password = txtpasswd.Text;
                    txtrtyppass.Focus();
                    break;
                case Keys.Up:
                    txtName.Focus();
                    break;


            }

        }

        private void txtlevel_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:

                    break;
                case Keys.Down:

                    break;
                case Keys.Up:
                    txtpasswd.Focus();
                    break;
            }

        }

        private void txtuse_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtdetails.Focus();
                    break;
                case Keys.Down:
                    txtdetails.Focus();
                    break;
                case Keys.Up:

                    break;
            }


        }

        private void txtdetails_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    cbMakeItDefault.Focus();
                    break;
                case Keys.Down:
                    cbMakeItDefault.Focus();
                    break;
                case Keys.Up:
                    txtName.Focus();
                    break;
            }

        }

        #endregion
      
        private void txtryppass_Leave(object sender, EventArgs e)
        {
            lblmesg.Text = "empty";
            if (_User.Password == txtrtyppass.Text)
            {
                // txtusrname.Focus();
                lblmesg.Text = "matches";
            }
            else
            {
                lblmesg.Text = "invalid pass";
            }
        }

        private void txtrtyppass_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (CheckPassword())
                    {
                        mcblevel.Focus();
                        lblmesg.Text = "";
                    }
                    else
                    {
                        txtrtyppass.Clear();
                        txtrtyppass.Focus();
                        lblmesg.Text = "Password Not Matches";
                    }
                    break;
                case Keys.Down:
                    if (CheckPassword())
                    {
                        mcblevel.Focus();
                        lblmesg.Text = "";
                    }
                    else
                    {
                        txtrtyppass.Clear();
                        txtrtyppass.Focus();
                        lblmesg.Text = "Password Not Matches";
                    }
                    break;
                case Keys.Up:
                    txtpasswd.Focus();
                    break;


            }
        }

        private void chkifuse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkifuse.Checked == true)
            {
                _User.IfInUse = 1;
            }
            else
            { _User.IfInUse = 0; }
            txtdetails.Focus();
        }

        private void mcblevel_Leave(object sender, EventArgs e)
        {
            txtdetails.Focus();
        }

      

        private void headerLabel1_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void mcblevel_EnterKeyPressed(object sender, EventArgs e)
        {
            txtdetails.Focus();
        }

        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (_Mode != OperationMode.Add)
                    FillSearchData(txtName.SelectedID,"");
                txtpasswd.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
        }

        private void cbMakeItDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMakeItDefault.Checked == true)
            {
                _User.MakeItDefault = "Y";
            }
            else
            {
                _User.MakeItDefault = "N";
            }
           
        }
       
    }
}
