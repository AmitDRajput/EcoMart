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
    public partial class UclCompany : BaseControl
    {
        #region Declaration       
        private Company _Company;        
        #endregion Declaration

        #region Constructors
        public UclCompany()
        {
            InitializeComponent();
            _Company = new Company();
            SearchControl = new UclCompanySearch();                   
        }        
        #endregion  Constructor
               

        #region IDetailControl Members
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Company.Initialise();         
            ClearControls();
            txtName.Focus();
            return true;
        }

        public override bool Add()
        {
            bool retValue = true;
            base.Add();
            ClearData();           
            panel1.Enabled = true;           
            FilltxtName();
            FillFirstCreditorCombo();
            FillSecondCreditorCombo();
            headerLabel1.Text = "COMPANY -> ADD";
            txtName.Focus();
            AddToolTip();
            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();           
            panel1.Enabled = true;
            headerLabel1.Text = "COMPANY -> EDIT";
            AddToolTip();
            FilltxtName();
            FillFirstCreditorCombo();
            FillSecondCreditorCombo();
            txtName.Focus();
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            panel1.Enabled = true;
            return true;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "COMPANY -> DELETE";
            ClearData();
            FilltxtName();
            FillFirstCreditorCombo();
            FillSecondCreditorCombo();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_Company.Id != null && _Company.Id != "")
            {
                retValue = _Company.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Company.DeleteDetails();
                    MessageBox.Show("Company information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            panel1.Enabled = true;
            ClearData();
            FilltxtName();
            FillFirstCreditorCombo();
            FillSecondCreditorCombo();
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            panel1.Enabled = true;
            ClearData();
            FilltxtName();            
            headerLabel1.Text = "COMPANY -> VIEW";
            return retValue;
        }       

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _Company.ShortName = txtShortName.Text.Trim();
            _Company.CName = txtName.Text.Trim();
            _Company.Address = txtAddress.Text.Trim();
            _Company.Telephone = txtTelephone.Text.Trim();
            _Company.MailID = txtMailId.Text.Trim();
            _Company.ContactPerson = txtContactPerson.Text.Trim();
            if (mcbFirstCreditor.SelectedID != null && mcbFirstCreditor.SelectedID != string.Empty)
                _Company.PartyID_1 = mcbFirstCreditor.SelectedID;
            if (mcbSecondCreditor.SelectedID != null && mcbSecondCreditor.SelectedID != string.Empty)
                _Company.PartyID_2 = mcbSecondCreditor.SelectedID;
            if (_Mode == OperationMode.Edit)
                _Company.IFEdit = "Y";
            _Company.Validate();         
                
            if (_Company.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode==OperationMode.OpenAsChild)
                {
                    _Company.CreatedBy = General.CurrentUser.Id;
                    _Company.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Company.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    _Company.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    retValue = _Company.AddDetails();
                    if (_Company.PartyID_1 != string.Empty)
                        retValue = _Company.UpdateProductMasterWithPartyID1();
                    if (_Company.PartyID_2 != string.Empty)
                        retValue = _Company.UpdateProductMasterWithPartyID1();
                    MessageBox.Show("Company information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Company.Id;
                    ClearControls();                    
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _Company.ModifiedBy = General.CurrentUser.Id;
                    _Company.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Company.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Company.UpdateDetails();
                    MessageBox.Show("Company information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Company.Id;
                    ClearControls();                   
                    retValue = true;
                }               
              
            }
            else // Show Validation Messages
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Company.ValidationMessages)
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
                _Company.Initialise();
                _Company.Id = ID;
                _Company.ReadDetailsByID();
                //Fill Controls
                txtShortName.Text = _Company.ShortName.Trim();
                txtName.Text = _Company.CName.Trim();
                txtTelephone.Text = _Company.Telephone.Trim();
                txtMailId.Text = _Company.MailID.Trim();
                txtAddress.Text = _Company.Address.Trim();
                txtContactPerson.Text = _Company.ContactPerson.Trim();
                if (_Company.PartyID_1 != string.Empty)
                    mcbFirstCreditor.SelectedID = _Company.PartyID_1;
                if (_Company.PartyID_2 != string.Empty)
                    mcbSecondCreditor.SelectedID = _Company.PartyID_2;
            }
            if (Mode == OperationMode.Delete || Mode == OperationMode.View || Mode == OperationMode.ReportView)
                panel1.Enabled = false;          
            return true;
        }
        
        public void FilltxtName()
        {            
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "CompID", "CompName" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            Company _Comp = new Company();
            DataTable dtable = _Comp.GetOverviewData();
            txtName.FillData(dtable);
        }
        private void FillFirstCreditorCombo()
        {
            mcbFirstCreditor.SelectedID = null;
            mcbFirstCreditor.SourceDataString = new string[2] { "AccountID", "AccName", };
            mcbFirstCreditor.ColumnWidth = new string[2] { "0", "200" };
            mcbFirstCreditor.ValueColumnNo = 0;
            mcbFirstCreditor.UserControlToShow = new UclAccount();
            Account _Account = new Account();
            DataTable dtable = _Account.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
            mcbFirstCreditor.FillData(dtable);
        }

        private void FillSecondCreditorCombo()
        {
            mcbSecondCreditor.SelectedID = null;
            mcbSecondCreditor.SourceDataString = new string[2] { "AccountID", "AccName", };
            mcbSecondCreditor.ColumnWidth = new string[2] { "0", "200" };
            mcbSecondCreditor.ValueColumnNo = 0;
            mcbSecondCreditor.UserControlToShow = new UclAccount();
            Account _Account = new Account();
            DataTable dtable = _Account.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
            mcbSecondCreditor.FillData(dtable);
        }
        #endregion IDetailControl

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
            if (keyPressed == Keys.C && modifier == Keys.Alt)
            {
                txtContactPerson.Focus();
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
                txtShortName.Focus();
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
         
        #endregion IDetail Members

        #region Other Private Methods
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
            //bool retValue = false;
            //if (_Company.CName != txtName.Text.Trim())
            //    retValue = true;
            //if (_Company.ShortName != txtShortName.Text.Trim())
            //    retValue = true;
            //if (_Company.Address != txtAddress.Text.Trim())
            //    retValue = true;
            //if (_Company.Telephone != txtTelephone.Text.Trim())
            //    retValue = true;
            //if (_Company.MailID != txtMailId.Text.Trim())
            //    retValue = true;
            //if (_Company.ContactPerson != txtContactPerson.Text.Trim())
            //    retValue = true;
            //return retValue;
        }
        private void ClearControls()
        {
            txtShortName.Clear();
            txtName.Text = "";
            txtAddress.Clear();
            txtContactPerson.Clear();
            txtMailId.Clear();
            txtTelephone.Clear();
            mcbFirstCreditor.SelectedID = "";
            mcbSecondCreditor.SelectedID = "";
        }
        #endregion

        #region Events     
       
        private void txtShortName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
              
                case Keys.Enter:
                    txtAddress.Focus();
                    break;
                case Keys.Down:
                    txtAddress.Focus();
                    break;
                case Keys.Up:
                    txtName.Focus();
                    break;
                
            }
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtShortName.Focus(); 
                    break;
                case Keys.Down:
                    txtShortName.Focus();
                    break;
                case Keys.Up:
                    txtName.Focus();
                    break;             
            }
        }
        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtTelephone.Focus();
                    break;
                case Keys.Down:
                    txtTelephone.Focus();
                    break;
                case Keys.Up:
                    txtName.Focus();
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
                    txtContactPerson.Focus();
                    break;
                case Keys.Down:
                    txtContactPerson.Focus();
                    break;
                case Keys.Up:
                    txtTelephone.Focus();
                    break;              
            }
        }
        private void txtContactPerson_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    mcbFirstCreditor.Focus();
                    //SetButtonFocus(OperationButton.Save);
                    break;             
                case Keys.Up:
                    txtMailId.Focus();
                    break;              
            }
        }
        private void txtMailId_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtMailId.Text.Trim().Length > 0)
            {
                if (!rEMail.IsMatch(txtMailId.Text.Trim()))
                {
                    MessageBox.Show("E-Mail expected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMailId.SelectAll();
                    e.Cancel = true;
                }
            }
        }
        
      
      

        private void headerLabel1_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {          
            FillSearchData(txtName.SelectedID,"");
            txtShortName.Focus();
        }
     
        #endregion  Events

        #region tooltip
        private void AddToolTip()
        {
            ttCompany.SetToolTip(txtName, "use A-Z,0-9,space only");
            ttCompany.SetToolTip(txtAddress, "Fill company Address");
            ttCompany.SetToolTip(txtShortName, "Maximum 3 letters");
            ttCompany.SetToolTip(txtContactPerson, "The person with whom you communicate");
            ttCompany.SetToolTip(txtMailId, "Enter Valid email Id");
            ttCompany.SetToolTip(txtTelephone, "Enter Tel.no and mobile number");

        }
        #endregion   

        private void mcbFirstCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbSecondCreditor.Focus();
        }

       
    }
}
