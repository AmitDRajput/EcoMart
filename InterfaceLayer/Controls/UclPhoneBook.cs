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
    public partial class UclPhoneBook : BaseControl
    {
        # region Declaration
         private PhoneBook _PhoneBook;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        # endregion Declaration

        # region constructor
        public UclPhoneBook()
        {
            InitializeComponent();
            _PhoneBook = new PhoneBook();
            SearchControl = new UclPhoneBookSearch();
        }
        # endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _PhoneBook.Initialise();
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
            headerLabel1.Text = "PHONE BOOK -> NEW";
            txtName.Focus();
            AddToolTip();
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            headerLabel1.Text = "PHONE BOOK -> EDIT";
            AddToolTip();
            FilltxtName();
            txtName.Focus();
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
            headerLabel1.Text = "PHONE BOOK -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            _PhoneBook.Id = txtName.SelectedID;
            if (_PhoneBook.Id != null && _PhoneBook.Id != "")
            {
                retValue = _PhoneBook.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _PhoneBook.DeleteDetails();
                    MessageBox.Show("PHONE BOOK information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("ID", "Name", "tblPhoneBook");
            bool retValue = base.View();
            panel1.Enabled = true;
            ClearData();
            FilltxtName();
            headerLabel1.Text = "PHONE BOOK -> VIEW";
            MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _PhoneBook.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_PhoneBook.Id, "");

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
                    _PhoneBook.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_PhoneBook.Id, "");
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
                _PhoneBook.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_PhoneBook.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _PhoneBook.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_PhoneBook.Id, "");
            return retValue;
        }

        public override bool Save()
        {
            
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _PhoneBook.Name = txtName.Text.Trim();
            _PhoneBook.Address1 = txtAddress1.Text.ToString();
            _PhoneBook.Address2 = txtAddress2.Text.ToString();
            _PhoneBook.Telephone = txtTelephone.Text.Trim();
            _PhoneBook.MobileNumberForSMS = txtMobileNumberForSMS.Text.Trim();
            _PhoneBook.EmailID = txtMailId.Text.Trim();
            _PhoneBook.Bday = txtdd.Text.ToString();
            _PhoneBook.Bmonth = txtmm.Text.ToString();
            _PhoneBook.Byear = txtyy.Text.ToString();
            _PhoneBook.Remark = txtRemark.Text.ToString();
            if (_Mode == OperationMode.Edit)
                _PhoneBook.IFEdit = "Y";
            _PhoneBook.Validate();
            if (_PhoneBook.IsValid)
            {
                LockTable.LockTableForPhoneBook();
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _PhoneBook.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _PhoneBook.CreatedBy = General.CurrentUser.Id;
                    _PhoneBook.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _PhoneBook.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _PhoneBook.AddDetails();
                    MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _PhoneBook.Id;
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _PhoneBook.ModifiedBy = General.CurrentUser.Id;
                    _PhoneBook.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _PhoneBook.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _PhoneBook.UpdateDetails();
                    MessageBox.Show("Information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _PhoneBook.Id;
                    retValue = true;
                }
            }
            else // Show Validation Messages
            {
                LockTable.UnLockTables();
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _PhoneBook.ValidationMessages)
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
                _PhoneBook.Id = ID;
                _PhoneBook.ReadDetailsByID();
                txtName.Text = _PhoneBook.Name.Trim();
                txtAddress1.Text = _PhoneBook.Address1.ToString();
                txtAddress2.Text = _PhoneBook.Address2.ToString();
                txtTelephone.Text = _PhoneBook.Telephone.ToString();
                txtMobileNumberForSMS.Text = _PhoneBook.MobileNumberForSMS.ToString();
                txtMailId.Text = _PhoneBook.EmailID.ToString();
                txtRemark.Text = _PhoneBook.Remark.ToString();
                txtdd.Text = _PhoneBook.Bday.ToString();
                txtmm.Text = _PhoneBook.Bmonth.ToString();
                txtyy.Text = _PhoneBook.Byear.ToString();
               
            }
            if (Mode == OperationMode.Delete || Mode == OperationMode.View)
                panel1.Enabled = false;
            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "ID", "Name" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            PhoneBook _Phone = new PhoneBook();
            DataTable dtable = _Phone.GetOverviewData();
            txtName.FillData(dtable);
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

            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                txtAddress1.Focus();
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
        public override bool IsDetailChanged()
        {
            bool retValue = true;
            //if (_PhoneBook.Name != txtName.Text.Trim())
            //    retValue = true;
            //if (_PhoneBook.DocAddress != txtAddress1.Text.Trim())
            //    retValue = true;
            //if (_PhoneBook.DocTelephone != txtTelephone.Text.Trim())
            //    retValue = true;
            //if (_PhoneBook.DocEmailID != txtMailId.Text.Trim())
            //    retValue = true;          

            return retValue;
        }
        private void ClearControls()
        {
            txtName.SelectedID = "";
            txtName.Text = "";
            txtAddress1.Clear();
            txtTelephone.Clear();
            txtMobileNumberForSMS.Text = "";
            txtMailId.Text = "";
            txtAddress2.Text = "";
            txtdd.Text = "";
            txtmm.Text = "";
            txtyy.Text = "";
            txtRemark.Text = "";
            txtMailId.Clear();
            tsBtnFifth.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSavenPrint.Visible = false;
        }
        #endregion OtherPrivate Methods

        #region Events
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtAddress1.Focus();              
                    break;
                case Keys.Down:
                    txtAddress1.Focus();
                    break;               
            }
        }
       
        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtAddress2.Focus();                  
                    break;
                case Keys.Down:
                    txtAddress2.Focus();
                    break;
                case Keys.Up:
                    txtName.Focus();
                    break;
            }
        }
        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
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
                    txtAddress1.Focus();
                    break;
            }
        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtMobileNumberForSMS.Focus();
                    break;
                case Keys.Down:
                    txtMobileNumberForSMS.Focus();
                    break;
                case Keys.Up:
                    txtAddress2.Focus();
                    break;
            }
        }
        private void txtMobileNumberForSMS_KeyDown(object sender, KeyEventArgs e)
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
                    txtTelephone.Focus();
                    break;
            }
        }

        private void txtMailId_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtdd.Focus();
                    break;
                case Keys.Down:
                    txtdd.Focus();
                    break;
                case Keys.Up:
                    txtMobileNumberForSMS.Focus();
                    break;
            }
        }
        private void txtdd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtmm.Focus();
                    break;
                case Keys.Down:
                    txtmm.Focus();
                    break;
                case Keys.Up:
                    txtMailId.Focus();
                    break;
            }
        }

        private void txtmm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtyy.Focus();
                    break;
                case Keys.Down:
                    txtyy.Focus();
                    break;
                case Keys.Up:
                    txtdd.Focus();
                    break;
            }
        }

        private void txtyy_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtRemark.Focus();
                    break;
                case Keys.Down:
                    txtRemark.Focus();
                    break;
                case Keys.Up:
                   txtmm.Focus();
                    break;
            }
        }
        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {

            FillSearchData(txtName.SelectedID, "");
            txtAddress1.Focus();
        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            //ttDoctor.SetToolTip(txtName, "A-Z,0-9,space only");
            //ttDoctor.SetToolTip(txtAddress, "Fill Full Postal address");
            //ttDoctor.SetToolTip(txtMailId, "Fill Valid Email ID");
            //ttDoctor.SetToolTip(txtTelephone, "Fill Telephone,Mobile Numbers");
            //ttDoctor.SetToolTip(txtNameAddress, "Fill Name,Address that will be printed on the Sale Bill");
        }
        #endregion 


       
    }
}
