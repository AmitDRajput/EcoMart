using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.InterfaceLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclOperator : BaseControl
    {
        # region declaration
        private Operator _Operator;
        #endregion

        #region  Constructor
        public UclOperator()
        {
            try
            {
                InitializeComponent();
                _Operator = new Operator();
                //     SearchControl = new UclOperatorSearch();
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
                _Operator.Initialise();
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
                FillUserName();
                chkifuse.Checked = true;
                headerLabel1.Text = "Operator -> NEW";
                txtName.Focus();
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
                ClearData();
                headerLabel1.Text = "Operator -> EDIT";
                //    AddToolTip();              
                FillUserName();
                chkifuse.Checked = true;
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
                headerLabel1.Text = "Operator -> DELETE";
                ClearData();              
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
                if (_Operator.Id != null && _Operator.Id != "")
                {
                    if (_Operator.CanBeDeleted())
                    {
                        _Operator.IfInUse = 0;
                        _Operator.DeleteDetails();
                        MessageBox.Show("Operator Information has been deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                ClearData();               
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
            bool retValue = base.View();
            try
            {
                ClearData();              
                FillUserName();
                txtName.Focus();
                headerLabel1.Text = "Operator -> VIEW";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                _Operator.Name = txtName.Text.Trim();
                _Operator.Password = txtpasswd.Text.Trim();
                _Operator.Description = txtdetails.Text.Trim();               
                if (_Mode == OperationMode.Edit)
                    _Operator.IFEdit = "Y";
                else
                    _Operator.IFEdit = "N";
                _Operator.Validate();
                if (_Operator.IsValid)
                {
                    if (_Mode == OperationMode.Add)
                    {
                        _Operator.CreatedBy = General.CurrentUser.Id;
                        _Operator.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Operator.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _Operator.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");

                        retValue = _Operator.AddDetails();
                        MessageBox.Show("Operator information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Operator.Id;
                        ClearControls();
                        retValue = true;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _Operator.ModifiedBy = General.CurrentUser.Id;
                        _Operator.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Operator.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Operator.UpdateDetails();
                        MessageBox.Show("Operator information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Operator.Id;
                        ClearControls();
                        retValue = true;
                    }
                }
                else // Show Validation Messages
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Operator.ValidationMessages)
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
                    _Operator.Id = ID;
                    _Operator.ReadDetailsByID();
                    txtName.Text = _Operator.Name;
                    txtpasswd.Text = _Operator.Password;
                    txtrtyppass.Text = _Operator.Password;
                    txtdetails.Text = _Operator.Description;
                    if (_Operator.IfInUse == 1)
                        chkifuse.Checked = true;
                    else
                        chkifuse.Checked = false;                  
                    txtName.Enabled = false;
                    txtpasswd.Focus();
                    _Operator.ModifiedDate = "" + DateTime.Today.ToString("yyyyMMdd");

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
            if (keyPressed == Keys.D && modifier == Keys.Alt)
            {
                txtdetails.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.N && modifier == Keys.Alt)
            {
                txtName.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.P && modifier == Keys.Alt)
            {
                txtpasswd.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.R && modifier == Keys.Alt)
            {
                txtrtyppass.Focus();
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


        public void ClearControls()
        {
            try
            {
                txtName.Text = "";
                txtName.Enabled = true;
                txtpasswd.Clear();
                txtrtyppass.Clear();
                txtdetails.Clear();               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        #region other Methods
       

        private void FillUserName()
        {
            try
            {
                txtName.SelectedID = null;
                txtName.SourceDataString = new string[2] { "OperatorID", "OperatorName" };
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
                _Operator.Password = txtpasswd.Text.Trim();
                _Operator.RetypePassword = txtrtyppass.Text.Trim();
                if (_Operator.Password == _Operator.RetypePassword)
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
                    _Operator.Password = txtpasswd.Text;
                    txtrtyppass.Focus();
                    break;
                case Keys.Down:
                    _Operator.Password = txtpasswd.Text;
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
       
        #endregion

        private void txtryppass_Leave(object sender, EventArgs e)
        {
            lblmesg.Text = "empty";
            if (_Operator.Password == txtrtyppass.Text)
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
                    txtName.Focus();
                    break;


            }
        }

        private void chkifuse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkifuse.Checked == true)
            {
                _Operator.IfInUse = 1;
            }
            else
            { _Operator.IfInUse = 0; }
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
    }
}
