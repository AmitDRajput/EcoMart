using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDoctor : BaseControl
    {
        #region Declaration       
        private Doctor _Doctor;  
        #endregion Declaration

        #region Constructor
        public UclDoctor()
        {
            InitializeComponent();
            _Doctor = new Doctor();
            SearchControl = new UclDoctorSearch();
        }      
        #endregion Constructor

      
        # region IDetail Control 
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Doctor.Initialise();
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
            headerLabel1.Text = "DOCTOR -> NEW";
            txtName.Focus();
            AddToolTip();
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            headerLabel1.Text = "DOCTOR -> EDIT";
            AddToolTip();
            FilltxtName();
            txtName.Focus();
            return retValue;          
        }

        public override  bool Cancel()
        {
            bool retValue = base.Cancel();
            panel1.Enabled = true;
            return retValue;
        }        

        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "DOCTOR -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;  
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            _Doctor.Id = txtName.SelectedID;
            if (_Doctor.Id != null && _Doctor.Id != "")
            {
                retValue = _Doctor.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Doctor.DeleteDetails();
                    MessageBox.Show("Doctor information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            bool retValue = base.View();
            panel1.Enabled = true;
            ClearData();
            FilltxtName();           
            headerLabel1.Text = "DOCTOR -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;       
            _Doctor.Name = txtName.Text.Trim();
            _Doctor.DocAddress = txtAddress.Text.Trim();
            _Doctor.DocTelephone = txtTelephone.Text.Trim();
            _Doctor.DocEmailID = txtMailId.Text.Trim();
            _Doctor.DocShortNameAddress = txtNameAddress.Text.Trim();
            _Doctor.DocRegistrationNumber = txtRegistrationNumber.Text.Trim();
            _Doctor.DocDegree = txtDegree.Text.Trim();
            if (_Mode == OperationMode.Edit)
                _Doctor.IFEdit = "Y";
            _Doctor.Validate();
            if (_Doctor.IsValid)
            {
                LockTable.LockTableForDoctor();
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _Doctor.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _Doctor.CreatedBy = General.CurrentUser.Id;
                    _Doctor.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Doctor.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Doctor.AddDetails();
                    MessageBox.Show("Doctor information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Doctor.Id; 
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _Doctor.ModifiedBy = General.CurrentUser.Id;
                    _Doctor.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Doctor.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Doctor.UpdateDetails();
                    MessageBox.Show("Doctor information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Doctor.Id;
                    retValue = true;
                }
            }
            else // Show Validation Messages
            {
                LockTable.UnLockTables();
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Doctor.ValidationMessages)
                {
                    _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                }
                MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            LockTable.UnLockTables();
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                _Doctor.Id = ID;
                _Doctor.ReadDetailsByID();
                txtName.Text = _Doctor.Name.Trim();
                txtAddress.Text = _Doctor.DocAddress.Trim();
                txtTelephone.Text = _Doctor.DocTelephone.Trim();
                txtMailId.Text = _Doctor.DocEmailID.Trim();
                txtNameAddress.Text = _Doctor.DocShortNameAddress.Trim();
                txtRegistrationNumber.Text = _Doctor.DocRegistrationNumber.Trim();
                txtDegree.Text = _Doctor.DocDegree.Trim();
            }
            if (Mode == OperationMode.Delete || Mode == OperationMode.View)
                panel1.Enabled = false;
            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "DocID", "DocName" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            Doctor _Doc = new Doctor();
            DataTable dtable = _Doc.GetOverviewData();
            txtName.FillData(dtable);
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
          
            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                txtAddress.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.I && modifier == Keys.Alt)
            {
                txtMailId.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.N && modifier == Keys.Alt)
            {
                txtName.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.S && modifier == Keys.Alt)
            {
                txtNameAddress.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.T && modifier == Keys.Alt)
            {
                txtTelephone.Focus();
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
        
        #endregion Idetail Members      

        #region Other private methods
        public override  bool IsDetailChanged()
        {
            bool retValue = false;
            if (_Doctor.Name != txtName.Text.Trim())
                retValue = true;
            if (_Doctor.DocAddress != txtAddress.Text.Trim())
                retValue = true;
            if (_Doctor.DocTelephone != txtTelephone.Text.Trim())
                retValue = true;
            if (_Doctor.DocEmailID != txtMailId.Text.Trim())
                retValue = true;
            if (_Doctor.DocShortNameAddress != txtNameAddress.Text.Trim())
                retValue = true;
            if (_Doctor.DocRegistrationNumber != txtRegistrationNumber.Text.Trim())
                retValue = true;
            if (_Doctor.DocDegree != txtDegree.Text.Trim())
                retValue = true;

            return retValue;
        }
        private void ClearControls()
        {
            txtName.SelectedID = "";
            txtName.Text = "";
            txtAddress.Clear();
            txtTelephone.Clear();
            txtMailId.Clear();
            txtNameAddress.Clear();
            txtRegistrationNumber.Clear();
            txtDegree.Clear();
        }
        #endregion OtherPrivate Methods

        #region Events
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtAddress.Focus();
                    txtNameAddress.Text = txtName.Text.Trim() + " " + txtAddress.Text.Trim();
                   
                    break;
                case Keys.Down:
                    txtAddress.Focus();
                    break;
                case Keys.Up:
                    txtNameAddress.Focus();
                    break;
            }
        }
        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtTelephone.Focus();
                    txtNameAddress.Text = txtName.Text.Trim() + " " + txtAddress.Text.ToUpper().Trim(); 
                    break;
                case Keys.Down:
                    txtTelephone.Focus();
                    break;
                case Keys.Up:
                    txtNameAddress.Focus();
                    break;          
            }
        }
        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtMailId.Focus();
                    break;
                case Keys.Down:
                    txtMailId.Focus();
                    break;
                case Keys.Up:
                    txtAddress.Focus();
                    break;         
            }
        }
        private void txtMailId_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtNameAddress.Focus();
                    break;
                case Keys.Down:
                    txtNameAddress.Focus();
                    break;
                case Keys.Up:
                    txtTelephone.Focus();
                    break;           
            }
        }
        private void txtNameAdd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtRegistrationNumber.Focus();
                    break;
                case Keys.Down:
                    txtDegree.Focus();
                    break;
                case Keys.Up:
                    txtMailId.Focus();
                    break;      
            }
        }

        private void txtRegistrationNumber_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtDegree.Focus();
                    break;
                case Keys.Down:
                    txtDegree.Focus();
                    break;
                case Keys.Up:
                    txtNameAddress.Focus();
                    break;
            }
        }
        private void txtDegree_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
               
                case Keys.Up:
                    txtRegistrationNumber.Focus();
                    break;
            }
        }
        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress.Text.ToString().Trim();
        }

        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {

            FillSearchData(txtName.SelectedID,"");
            txtAddress.Focus();
        }
        #endregion Events      

        #region tooltip
        private void AddToolTip()
        {
           ttDoctor.SetToolTip(txtName,"A-Z,0-9,space only");
           ttDoctor.SetToolTip(txtAddress, "Fill Full Postal address");
           ttDoctor.SetToolTip(txtMailId, "Fill Valid Email ID");
           ttDoctor.SetToolTip(txtTelephone, "Fill Telephone,Mobile Numbers");
           ttDoctor.SetToolTip(txtNameAddress, "Fill Name,Address that will be printed on the Sale Bill");
        }
        #endregion       
        
    }
}
