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
    public partial class UclDelivaryBoy : BaseControl
    {

        #region Declaration        
        private DelivaryBoy _DelivaryBoy;

        Hashtable htTableList;
        public int CurrentNumber = 0;
        # endregion

        public UclDelivaryBoy()
        {
            InitializeComponent();
            _DelivaryBoy = new DelivaryBoy();
            SearchControl = new UclDelivaryBoySearch();
        }

        private void UclDelivaryBoy_Load(object sender, EventArgs e)
        {

        }


        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            panel1.Enabled = true;
            FilltxtName();
            AddToolTip();
            headerLabel1.Text = "DelivaryBouy -> NEW";
            txtName.Focus();
            return retValue;
        }

        #region tooltip
        private void AddToolTip()
        {
            //questions..
            // ttDelivaryBoy.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "ID", "Name" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            DelivaryBoy _txt = new DelivaryBoy();
            DataTable dtable = _txt.GetOverviewData();
            txtName.FillData(dtable);
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


        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            AddToolTip();
            headerLabel1.Text = "DELIVARYBOY -> EDIT";
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
            headerLabel1.Text = "DELIVARYBOY -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_DelivaryBoy.Id != null && _DelivaryBoy.Id != "")
            {
                retValue = _DelivaryBoy.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _DelivaryBoy.DeleteDetails();
                    MessageBox.Show("DelivaryBoy information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("ID", "Name", "masterdelivaryboy");
            bool retValue = base.View();
            ClearData();
            FilltxtName();
            headerLabel1.Text = "DelivaryBoy -> VIEW";
            MoveLast();
            return retValue;
        }

        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _DelivaryBoy.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_DelivaryBoy.Id, "");

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
                    _DelivaryBoy.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_DelivaryBoy.Id, "");
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
                _DelivaryBoy.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_DelivaryBoy.Id, "");
            return retValue;
        }


        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _DelivaryBoy.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_DelivaryBoy.Id, "");
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _DelivaryBoy.Name = txtName.Text;
            _DelivaryBoy.Address1 = txtDocAddress1.Text;
            _DelivaryBoy.Address2 = txtDocAddress2.Text;
            _DelivaryBoy.Telephone = txtTelephone.Text;
            _DelivaryBoy.MobileNumberForSMS = txtMobileNumberForSMS.Text;
            _DelivaryBoy.MailID = txtMailId.Text;



            if (_Mode == OperationMode.Edit)
                _DelivaryBoy.IFEdit = "Y";
            _DelivaryBoy.Validate();
            if (_DelivaryBoy.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _DelivaryBoy.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _DelivaryBoy.CreatedBy = General.CurrentUser.Id;
                    _DelivaryBoy.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _DelivaryBoy.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _DelivaryBoy.AddDetails();
                    MessageBox.Show("DelivaryBoy information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _DelivaryBoy.Id;
                    ClearControls();
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _DelivaryBoy.ModifiedBy = General.CurrentUser.Id;
                    _DelivaryBoy.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _DelivaryBoy.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _DelivaryBoy.UpdateDetails();
                    MessageBox.Show("DelivaryBoy information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _DelivaryBoy.Id;
                    ClearControls();
                    retValue = true;
                }
            }
            else // Show Validation Salesmans
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _DelivaryBoy.ValidationMessages)
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
                _DelivaryBoy.Id = ID;
                _DelivaryBoy.ReadDetailsByID(Convert.ToInt32(ID));
                txtName.Text = _DelivaryBoy.Name;
                txtDocAddress1.Text = _DelivaryBoy.Address1;
                txtDocAddress2.Text = _DelivaryBoy.Address2;
                txtTelephone.Text = _DelivaryBoy.Telephone;
                txtMobileNumberForSMS.Text = _DelivaryBoy.MobileNumberForSMS;
                txtMailId.Text = _DelivaryBoy.MailID;
                txtName.Focus();
            }

            return true;
        }


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
        #endregion IDetail Members

        #region other Private Methods
        public override bool IsDetailChanged()
        {
            bool retValue = false;
            if (_DelivaryBoy.Name != null && _DelivaryBoy.Name != txtName.Text)
                retValue = true;
            return retValue;
        }
        #endregion

        private void txtName_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (txtName.SelectedID != null && txtName.SelectedID != "")
                FillSearchData(txtName.SelectedID, "");
        }



    }
}
