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
    public partial class UclMasterDelivarySalesman : BaseControl
    {
        #region Declaration        
        private DelivarySalesman _MasterDelivarySalesman;
        

        Hashtable htTableList;
        public int CurrentNumber = 0;
        # endregion

        # region Constructor
        public UclMasterDelivarySalesman()
        {
            InitializeComponent();
            _MasterDelivarySalesman = new DelivarySalesman();

            SearchControl = new uclMasterDelivarySalesmanSearch();
        }
        #endregion


        #region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _MasterDelivarySalesman.Initialise();
            ClearControls();
            txtName.Focus();
            return true;
        }

        public override bool Exit()
        {
            return base.Exit();
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            panel1.Enabled = true;
            FilltxtName();
            AddToolTip();
            headerLabel1.Text = "DelivarySalesman -> NEW";
            txtName.Focus();
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            AddToolTip();
            headerLabel1.Text = "DelivarySalesman -> EDIT";
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
            headerLabel1.Text = "DelivarySalesman -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_MasterDelivarySalesman.Id != null && _MasterDelivarySalesman.Id != "")
            {
                retValue = _MasterDelivarySalesman.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _MasterDelivarySalesman.DeleteDetails();
                    MessageBox.Show("MasterDelivarySalesman information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("ID", "Name", "masterdelivarysalesman");
            bool retValue = base.View();
            ClearData();
            FilltxtName();
            headerLabel1.Text = "DelivarySalesman -> VIEW";
            MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _MasterDelivarySalesman.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_MasterDelivarySalesman.Id, "");

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
                    _MasterDelivarySalesman.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_MasterDelivarySalesman.Id, "");
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
                _MasterDelivarySalesman.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_MasterDelivarySalesman.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _MasterDelivarySalesman.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_MasterDelivarySalesman.Id, "");
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _MasterDelivarySalesman.Name = txtName.Text;
            _MasterDelivarySalesman.Address1 = txtDocAddress1.Text;
            _MasterDelivarySalesman.Address2 = txtDocAddress2.Text;
            _MasterDelivarySalesman.Telephone = txtTelephone.Text;
            _MasterDelivarySalesman.MobileNumberForSMS = txtMobileNumberForSMS.Text;
            _MasterDelivarySalesman.MailID = txtMailId.Text;



            if (_Mode == OperationMode.Edit)
                _MasterDelivarySalesman.IFEdit = "Y";
            _MasterDelivarySalesman.Validate();
            if (_MasterDelivarySalesman.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _MasterDelivarySalesman.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _MasterDelivarySalesman.CreatedBy = General.CurrentUser.Id;
                    _MasterDelivarySalesman.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _MasterDelivarySalesman.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _MasterDelivarySalesman.AddDetails();
                    MessageBox.Show("MasterDelivarySalesman information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _MasterDelivarySalesman.Id;
                    ClearControls();
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _MasterDelivarySalesman.ModifiedBy = General.CurrentUser.Id;
                    _MasterDelivarySalesman.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _MasterDelivarySalesman.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _MasterDelivarySalesman.UpdateDetails();
                    MessageBox.Show("MasterDelivarySalesman information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _MasterDelivarySalesman.Id;
                    ClearControls();
                    retValue = true;
                }
            }
            else // Show Validation Salesmans
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _MasterDelivarySalesman.ValidationMessages)
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
                _MasterDelivarySalesman.Id = ID;
                _MasterDelivarySalesman.ReadDetailsByID(Convert.ToInt32(ID));
                txtName.Text = _MasterDelivarySalesman.Name;
                txtDocAddress1.Text = _MasterDelivarySalesman.Address1;
                txtDocAddress2.Text = _MasterDelivarySalesman.Address2;
                txtTelephone.Text = _MasterDelivarySalesman.Telephone;
                txtMobileNumberForSMS.Text = _MasterDelivarySalesman.MobileNumberForSMS;
                txtMailId.Text = _MasterDelivarySalesman.MailID;
                txtName.Focus();
            }

            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "ID", "Name" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            DelivarySalesman _txt = new DelivarySalesman();
            DataTable dtable = _txt.GetOverviewData();
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
            if (_MasterDelivarySalesman.Name != null && _MasterDelivarySalesman.Name != txtName.Text)
                retValue = true;
            return retValue;
        }
        private void ClearControls()
        {
            txtName.Text = "";
            txtName.SelectedID = "";
            txtName.Focus();
            txtDocAddress1.Text = "";
            txtDocAddress2.Text = "";
            txtTelephone.Text = "";
            txtMobileNumberForSMS.Text = "";
            txtMailId.Text = "";


            tsBtnFifth.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSavenPrint.Visible = false;
        }
        #endregion


        # region Events

        private void txtName_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (txtName.SelectedID != null && txtName.SelectedID != "")
                FillSearchData(txtName.SelectedID, "");
        }

        #endregion


        #region tooltip
        private void AddToolTip()
        {
            //ttSalesman.SetToolTip(txtName, "A-Z,0-9,space only");
        }


        #endregion

        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            FillSearchData(txtName.SelectedID, "");
            txtDocAddress1.Focus();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtDocAddress1.Focus();
                    break;
                case Keys.Down:
                    txtDocAddress1.Focus();
                    break;
                case Keys.Up:
                    txtName.Focus();
                    break;
            }
        }

        private void txtDocAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtDocAddress2.Focus();
                    break;
                case Keys.Down:
                    txtDocAddress2.Focus();
                    break;
                case Keys.Up:
                    txtDocAddress1.Focus();
                    break;
            }
        }

        private void txtDocAddress2_KeyDown(object sender, KeyEventArgs e)
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
                    txtDocAddress2.Focus();
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
                    txtTelephone.Focus();
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

            }
        }

        private void txtDocAddress1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtName.Focus();
                    break;
            }
        }

        private void txtDocAddress2_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtDocAddress1.Focus();
                    break;
            }
        }

        private void txtTelephone_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtDocAddress2.Focus();
                    break;
            }
        }

        private void txtMobileNumberForSMS_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtTelephone.Focus();
                    break;
            }
        }

        private void txtMailId_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtMobileNumberForSMS.Focus();
                    break;
            }
        }
    }
}
