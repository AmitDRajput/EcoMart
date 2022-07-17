using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using System.Collections;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclCompany : BaseControl
    {
        #region Declaration       
        private Company _Company;
        private PartyCompany _PartyCompany;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        #endregion Declaration

        #region Constructors
        public UclCompany()
        {
            InitializeComponent();
            _Company = new Company();
            _PartyCompany = new PartyCompany();
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
            FillThirdCreditorCombo();//Amar
            FillForthCreditorCombo();
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
            FillThirdCreditorCombo(); //Amar
            FillForthCreditorCombo();
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
            FillThirdCreditorCombo(); //Amar
            FillForthCreditorCombo();
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
            FillThirdCreditorCombo();
            FillForthCreditorCombo();
            return true;
        }

        public override bool View()
        {
            htTableList = General.GetTableListByCode("CompID", "CompName", "MasterCompany");
            bool retValue = base.View();
            panel1.Enabled = true;
            ClearData();
            FilltxtName();
            headerLabel1.Text = "COMPANY -> VIEW";
            // FillSearchData("0000063343C04D429D07AC3EE1F90072", "");
            //  FillSearchData( "");
            MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Company.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Company.Id, "");

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
                    _Company.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Company.Id, "");
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
                _Company.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Company.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Company.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Company.Id, "");
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
            //Amar
            if (mcbThirdCreditor.SelectedID!=null && mcbThirdCreditor.SelectedID!=string.Empty)
                _Company.PartyID_3 = mcbThirdCreditor.SelectedID;
            if (mcbForthCreditor.SelectedID != null && mcbForthCreditor.SelectedID != string.Empty)
                _Company.PartyID_4 = mcbForthCreditor.SelectedID; //End

            if (_Mode == OperationMode.Edit)
                _Company.IFEdit = "Y";
            _Company.Validate();

            if (_Company.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _Company.CreatedBy = General.CurrentUser.Id;
                    _Company.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Company.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    //_Company.IntID  = _Company.GetIntID(); /* Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/
                    //retValue = _Company.AddDetails();
                    _Company.IntID = _Company.GetNextIntID();
                    if (_Company.IntID > 0)
                        retValue = _Company.AddDetails();
                    if (retValue)
                    {
                        MessageBox.Show("Company information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SaveIntID = _Company.IntID;
                        ClearControls();
                        FilltxtName();
                        retValue = true;

                        if (_Company.PartyID_1 != string.Empty)
                        {
                            retValue = _Company.UpdateProductMasterWithPartyID1();
                            _PartyCompany.Id = _Company.PartyID_1;
                            _PartyCompany.CompanyId = _Company.Id;
                            _PartyCompany.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            retValue = _PartyCompany.AddDetails();
                        }
                        //if (_Company.PartyID_2 != string.Empty)
                        //{
                        //    retValue = _Company.UpdateProductMasterWithPartyID1();
                        //    _PartyCompany.Id = _Company.PartyID_2;
                        //    _PartyCompany.CompanyId = _Company.Id;
                        //    _PartyCompany.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        //    retValue = _PartyCompany.AddDetails();
                        //}

                        //if (_Company.PartyID_3 != string.Empty) //Amar
                        //{
                        //    retValue = _Company.UpdateProductMasterWithPartyID1();
                        //    _PartyCompany.Id = _Company.PartyID_3;
                        //    _PartyCompany.CompanyId = _Company.Id;
                        //    _PartyCompany.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        //    retValue = _PartyCompany.AddDetails();
                        //}

                        //if (_Company.PartyID_4 != string.Empty) //Amar
                        //{
                        //    retValue = _Company.UpdateProductMasterWithPartyID1();
                        //    _PartyCompany.Id = _Company.PartyID_4;
                        //    _PartyCompany.CompanyId = _Company.Id;
                        //    _PartyCompany.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        //    retValue = _PartyCompany.AddDetails();
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Unable to generate Company ID.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        retValue = false;
                    }
                    //MessageBox.Show("Company information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //_SavedID = _Company.Id;
                    //ClearControls();
                    //retValue = true;
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
                ClearControls();
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
                //Amar
                if (_Company.PartyID_3!=string.Empty)
                    mcbThirdCreditor.SelectedID = _Company.PartyID_3;
                if (_Company.PartyID_4 != string.Empty)
                    mcbForthCreditor.SelectedID = _Company.PartyID_4;
                //end
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

        private void FillThirdCreditorCombo()  //Amar
        {
            mcbThirdCreditor.SelectedID = null;
            mcbThirdCreditor.SourceDataString = new string[2] { "AccountID", "AccName", };
            mcbThirdCreditor.ColumnWidth = new string[2] { "0", "200" };
            mcbThirdCreditor.ValueColumnNo = 0;
            mcbThirdCreditor.UserControlToShow = new UclAccount();
            Account _Account = new Account();
            DataTable dtable = _Account.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
            mcbThirdCreditor.FillData(dtable);
        }

        private void FillForthCreditorCombo()  //Amar
        {
            mcbForthCreditor.SelectedID = null;
            mcbForthCreditor.SourceDataString = new string[2] { "AccountID", "AccName", };
            mcbForthCreditor.ColumnWidth = new string[2] { "0", "200" };
            mcbForthCreditor.ValueColumnNo = 0;
            mcbForthCreditor.UserControlToShow = new UclAccount();
            Account _Account = new Account();
            DataTable dtable = _Account.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
            mcbForthCreditor.FillData(dtable);
        }
        #endregion IDetailControl

        #region IDetail Members

        public override void ReFillData(Control closedControl)
        {
            FillFirstCreditorCombo();
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
            tsBtnFifth.Visible = false;
            tsBtnSavenPrint.Visible = false;
            txtShortName.Clear();
            txtName.Text = "";
            txtAddress.Clear();
            txtContactPerson.Clear();
            txtMailId.Clear();
            txtTelephone.Clear();
            mcbFirstCreditor.SelectedID = "";
            mcbSecondCreditor.SelectedID = "";
            mcbThirdCreditor.SelectedID = ""; //Amar
            mcbForthCreditor.SelectedID = "";//Amar
            tsBtnFifth.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSavenPrint.Visible = false;
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
            try
            {
                FillSearchData(txtName.SelectedID, "");
                string CompanyName = txtName.Text;
                if (string.IsNullOrEmpty(Convert.ToString(txtName.SelectedID)) == true && _Mode == OperationMode.Add && string.IsNullOrEmpty(CompanyName) == false)
                {
                    string ShortName = string.Empty;
                    if (CompanyName.Length > 3)
                        ShortName = CompanyName.Substring(0, 3);
                    else
                        ShortName = CompanyName.Substring(0, CompanyName.Length);
                    txtShortName.Text = ShortName;
                }
                txtShortName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbFirstCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbSecondCreditor.Focus();
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

        private void mcbSecondCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbThirdCreditor.Focus();
        }

        private void mcbThirdCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbForthCreditor.Focus();
        }

        private void mcbForthCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            MainToolStrip.Focus();
            tsBtnSave.Select();
        }

        private void mcbSecondCreditor_KeyDown(object sender, KeyEventArgs e) //Amar
        {
            if (e.KeyCode==Keys.Enter)
            {               
                    mcbThirdCreditor.Focus();
                      
            }
            if (e.KeyCode== Keys.Up)
            {
                mcbFirstCreditor.Focus();
            }
        }

        private void mcbThirdCreditor_KeyDown(object sender, KeyEventArgs e) //Amar
        {
            if (e.KeyCode == Keys.Enter)
            {
                mcbForthCreditor.Focus();

            }
            if (e.KeyCode == Keys.Up)
            {
                mcbSecondCreditor.Focus();
            }
        }

        private void mcbForthCreditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainToolStrip.Focus();
                tsBtnSave.Select();

            }
            if (e.KeyCode == Keys.Up)
            {
                mcbThirdCreditor.Focus();
            }
        }

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
             (e.KeyChar != ',') && (e.KeyChar != '/'))
            {
                e.Handled = true;
            }
        }
    }
}
