//Description : This class contains all methods required for Salesman master. 
//              This is user control required for Add/Update/Delete Sale//Description : This class contains all methods required for Salesman master.sman details


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
    public partial class UclSalesman : BaseControl 
    {
        # region Declaration        
        private Salesman _Salesman;
        

        Hashtable htTableList;
        public int CurrentNumber = 0;
        # endregion

        # region Constructor
        public UclSalesman()
        {
            InitializeComponent();
            _Salesman = new Salesman();
            
            SearchControl = new UclSalesmanSearch();
        }
        #endregion

       
        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Salesman.Initialise();
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
            headerLabel1.Text = "SALESMAN -> NEW";
            txtName.Focus();
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            AddToolTip();
            headerLabel1.Text = "SALESMAN -> EDIT";
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
            headerLabel1.Text = "SALESMAN -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;          
            if (_Salesman.Id != null && _Salesman.Id != "")
            {
                retValue = _Salesman.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Salesman.DeleteDetails();
                    MessageBox.Show("Salesman information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("SalesmanID", "SalesmanName", "mastersalesman");
            bool retValue = base.View();
            ClearData();
            FilltxtName();
            headerLabel1.Text = "SALESMAN -> VIEW";
            MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Salesman.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Salesman.Id, "");

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
                    _Salesman.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Salesman.Id, "");
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
                _Salesman.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Salesman.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Salesman.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Salesman.Id, "");
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
           _Salesman.Name = txtName.Text;
           _Salesman.Address1 = txtDocAddress1.Text;
           _Salesman.Address2 = txtDocAddress2.Text;
           _Salesman.Telephone = txtTelephone.Text;
           _Salesman.MobileNumberForSMS = txtMobileNumberForSMS.Text;
           _Salesman.MailID = txtMailId.Text;



            if (_Mode == OperationMode.Edit)
                _Salesman.IFEdit = "Y";
            _Salesman.Validate();
            if (_Salesman.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    
                    _Salesman.CreatedBy = General.CurrentUser.Id;
                    _Salesman.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Salesman.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    //retValue = _Salesman.AddDetails();
                    //MessageBox.Show("Salesman information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //_SavedID = _Salesman.Id;
                    //ClearControls();
                    //retValue = true;
                    _Salesman.IntID = _Salesman.AddDetails();
                    if (_Salesman.IntID > 0)
                    {
                        MessageBox.Show("Product Category information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SaveIntID = _Salesman.IntID;
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
                    _Salesman.ModifiedBy = General.CurrentUser.Id;
                    _Salesman.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Salesman.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Salesman.UpdateDetails();
                    MessageBox.Show("Salesman information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Salesman.Id;
                    ClearControls();
                    retValue = true;
                }
            }
            else // Show Validation Salesmans
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Salesman.ValidationMessages)
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
                _Salesman.Id = ID;
                _Salesman.ReadDetailsByID(Convert.ToInt32(ID));
                txtName.Text = _Salesman.Name;
                txtDocAddress1.Text = _Salesman.Address1;
                txtDocAddress2.Text = _Salesman.Address2;
                txtTelephone.Text = _Salesman.Telephone;
                txtMobileNumberForSMS.Text = _Salesman.MobileNumberForSMS;
                txtMailId.Text = _Salesman.MailID;
                txtName.Focus();
            }

            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "ID", "SalesmanName" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            Salesman _txt = new Salesman();
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
            if (_Salesman.Name != null && _Salesman.Name != txtName.Text)
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
                FillSearchData(txtName.SelectedID,"");
        }

        #endregion


        #region tooltip
        private void AddToolTip()
        {
            ttSalesman.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion
              

        private void txtDocAddress1_EnterKeyPressed(object sender, EventArgs e)
        {
            
            txtDocAddress2.Focus();
        }

        private void txtDocAddress2_EnterKeyPressed(object sender, EventArgs e)
        {
            txtTelephone.Focus();
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

        //private void txtDocAddress1_KeyDown(object sender, KeyEventArgs e)
        //{
         
        //}

        //private void txtDocAddress2_KeyDown(object sender, KeyEventArgs e)
        //{
          
        //}

        //private void txtMobileNumberForSMS_KeyDown(object sender, KeyEventArgs e)
        //{
          
        //}

        //private void txtDocAddress1_KeyUp(object sender, KeyEventArgs e)
        //{
         
        //}

        //private void txtDocAddress2_KeyUp(object sender, KeyEventArgs e)
        //{
         
        //}

        private void txtTelephone_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtDocAddress2.Focus();
                    break;
            }
        }

        //private void txtDocAddress1_KeyPress(object sender, KeyPressEventArgs e)
        //{
           
        //}

        //private void txtDocAddress1_KeyDown_1(object sender, KeyEventArgs e)
        //{
          
        //}

        private void txtDocAddress2_KeyDown_1(object sender, KeyEventArgs e)
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

        //private void txtDocAddress1_KeyUp_1(object sender, KeyEventArgs e)
        //{
        
        //}

        //private void txtDocAddress2_KeyUp_1(object sender, KeyEventArgs e)
        //{
          

        //}

        private void txtMobileNumberForSMS_KeyDown_1(object sender, KeyEventArgs e)
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

        //private void txtMobileNumberForSMS_KeyUp(object sender, KeyEventArgs e)
        //{
           
        //}

        //private void txtMailId_KeyUp(object sender, KeyEventArgs e)
        //{
         
        //}

        private void txtMobileNumberForSMS_KeyDown_2(object sender, KeyEventArgs e)
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

        private void txtMobileNumberForSMS_KeyUp_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtTelephone.Focus();
                    break;
            }
        }
               

        private void txtName_EnterKeyPressed_1(object sender, EventArgs e)
        {
            FillSearchData(txtName.SelectedID, "");
            txtDocAddress1.Focus();
        }

        private void txtDocAddress1_KeyDown_2(object sender, KeyEventArgs e)
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

        private void txtDocAddress1_KeyUp_2(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtName.Focus();
                    break;
            }
        }

        private void txtDocAddress2_KeyDown_2(object sender, KeyEventArgs e)
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

        private void txtDocAddress2_KeyUp_2(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtDocAddress1.Focus();
                    break;
            }
        }

        private void txtMailId_KeyUp_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    txtMobileNumberForSMS.Focus();
                    break;
            }
        }

        //private void MMMainPanel_Paint(object sender, PaintEventArgs e)
        //{

        //}
    }
}
