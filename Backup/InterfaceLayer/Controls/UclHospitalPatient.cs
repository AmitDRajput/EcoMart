﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.Common;


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclHospitalPatient : BaseControl
    {
        # region Declaration
        private HospitalPatient _HospitalPatient;
        # endregion Declaration

        # region constructor
        public UclHospitalPatient()
        {
            try
            {
                InitializeComponent();
                _HospitalPatient = new HospitalPatient();
                SearchControl = new UclHospitalPatientSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.UclPatient>>" + Ex.Message);
            }
        }
        # endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            txtInwardNumber.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _HospitalPatient.Initialise();
                ClearControls();
                txtInwardNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.ClearData>>" + Ex.Message);
            }
            return true;
        }
        public override bool Add()
        {
            bool retValue = base.Add();
            try
            {
                ClearData();
                FilltxtInwardNumber();
                FillDoctorCombo();
                FillWardCombo();
                FillAccountCombo();
                AddToolTip();
                headerLabel1.Text = "HOSPITAL PATIENT -> NEW";
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Add>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                headerLabel1.Text = "HOSPITAL PATIENT -> EDIT";
                AddToolTip();
                FilltxtInwardNumber();                
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Edit>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool Cancel()
        {
            bool retValue = false;
            try
            {
                retValue = base.Cancel();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Cancel>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool Delete()
        {
            try
            {
                bool retValue = base.Delete();
                headerLabel1.Text = "HOSPITAL PATIENT -> DELETE";
                ClearData();
                FilltxtInwardNumber();
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Delete>>" + Ex.Message);
            }
            return true;
        }
        public override bool ProcessDelete()
        {
            bool retValue = true;
            try
            {
                _HospitalPatient = new HospitalPatient();
                _HospitalPatient.Id = txtInwardNumber.SelectedID;
                if (_HospitalPatient.CanBeDeleted())
                {
                    _HospitalPatient.DeleteDetails();
                    MessageBox.Show("Patient information has been deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    retValue = true;
                }
                else
                {
                    MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearData();
                FilltxtInwardNumber();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.ProcessDelete>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool View()
        {
            bool retValue = false;
            try
            {
                retValue = base.View();
                ClearData();
                FilltxtInwardNumber();
                headerLabel1.Text = "HOSPITAL PATIENT -> VIEW";
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.View>>" + Ex.Message);
            }
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                if (txtInwardNumber.Text != null && txtInwardNumber.Text.ToString() != "")
                    _HospitalPatient.InwardNumber = txtInwardNumber.Text.ToString().Trim();
                _HospitalPatient.Name = txtName.Text.Trim();
                _HospitalPatient.Address1 = txtAddress1.Text;
                _HospitalPatient.Address2 = txtAddress2.Text;               
                if (txtTelephone.Text != null && txtTelephone.Text.ToString() != "")
                    _HospitalPatient.Telephone = txtTelephone.Text.ToString().Trim();
                if (txtEmailId.Text != null && txtEmailId.Text.ToString() != "")
                    _HospitalPatient.Email = txtEmailId.Text.ToString().Trim();
                if (txtNameAddress.Text != null && txtNameAddress.Text.ToString() != "")
                    _HospitalPatient.ShortNameAddress = txtNameAddress.Text.ToString().Trim();
                if (txtdd.Text != null && txtdd.Text.ToString().Trim() != "")
                    _HospitalPatient.Bday = Convert.ToInt32(txtdd.Text.ToString().Trim());
                if (txtmm.Text != null &&  txtmm.Text.ToString().Trim() != "")
                    _HospitalPatient.Bmonth = Convert.ToInt32(txtmm.Text.ToString().Trim());
                if (txtyy.Text != null && txtyy.Text.Trim() != null & txtyy.Text.Trim() != "")
                    _HospitalPatient.Byear = Convert.ToInt32(txtyy.Text.ToString().Trim());
                if (txtAgeYears.Text != null && txtAgeYears.Text.ToString().Trim() != "")
                    _HospitalPatient.Ageyears = Convert.ToInt32(txtAgeYears.Text.ToString().Trim());
                if (txtAgeMonths.Text != null && txtAgeMonths.Text.Trim() != "")
                    _HospitalPatient.Agemonths = Convert.ToInt32(txtAgeMonths.Text.ToString().Trim());
                if (txtAgeDays.Text != null && txtAgeDays.Text.ToString().Trim() != "")
                    _HospitalPatient.Agedays = Convert.ToInt32(txtAgeDays.Text.ToString().Trim());
                if (rbMale.Checked == true)
                    _HospitalPatient.Gender = "M";
                else
                    _HospitalPatient.Gender = "F";
                if (txtRoomNumber.Text != null && txtRoomNumber.Text.ToString() != "")
                    _HospitalPatient.RoomNumber = txtRoomNumber.Text.ToString();
                if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                    _HospitalPatient.DoctorID = mcbDoctor.SelectedID.Trim();
                if (mcbAccount.SelectedID != null && mcbAccount.SelectedID != "")
                    _HospitalPatient.AccountID = mcbAccount.SelectedID.Trim();
                if (txtIDNumber.Text != null && txtIDNumber.Text.ToString() != "")
                    _HospitalPatient.IDNumber = txtIDNumber.Text.ToString();
                if (txtRemark1.Text != null && txtRemark1.Text.ToString() != "")
                    _HospitalPatient.Remark1 = txtRemark1.Text.ToString();
                if (txtRemark2.Text != null && txtRemark2.Text.ToString() != "")
                    _HospitalPatient.Remark2 = txtRemark2.Text.ToString();
                if (txtRemark3.Text != null && txtRemark3.Text.ToString() != "")
                    _HospitalPatient.Remark3 = txtRemark3.Text.ToString();

                if (_Mode == OperationMode.Edit)
                    _HospitalPatient.IFEdit = "Y";
                _HospitalPatient.Name = txtName.Text;
                _HospitalPatient.Validate();
                if (_HospitalPatient.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _HospitalPatient.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _HospitalPatient.CreatedBy = General.CurrentUser.Id;
                        _HospitalPatient.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _HospitalPatient.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _HospitalPatient.AddDetails();

                        if (retValue == true)
                        {
                            MessageBox.Show("Saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _HospitalPatient.Id;
                        }
                        else
                        {
                            MessageBox.Show("Unable to save Patient information ", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        ClearControls();
                        retValue = true;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _HospitalPatient.ModifiedBy = General.CurrentUser.Id;
                        _HospitalPatient.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _HospitalPatient.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");                      
                        retValue = _HospitalPatient.UpdateDetails();
                        MessageBox.Show("Patient information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _HospitalPatient.Id;
                        retValue = true;
                    }
                }
                else
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _HospitalPatient.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.Save>>" + Ex.Message);
            }
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _HospitalPatient.Id = ID;

                    _HospitalPatient.ReadDetailsByID();
                    FillDoctorCombo();
                    FillWardCombo();
                    FillAccountCombo();
                    txtName.Text = _HospitalPatient.Name.Trim();
                    txtAddress1.Text = _HospitalPatient.Address1.Trim();
                    txtAddress2.Text = _HospitalPatient.Address2.Trim();
                    txtTelephone.Text = _HospitalPatient.Telephone.Trim();
                    txtEmailId.Text = _HospitalPatient.Email.Trim();
                    txtdd.Text = _HospitalPatient.Bday.ToString().Trim();
                    txtmm.Text = _HospitalPatient.Bmonth.ToString().Trim();
                    txtyy.Text = _HospitalPatient.Byear.ToString().Trim();
                    txtNameAddress.Text = _HospitalPatient.ShortNameAddress.Trim();
                    txtRemark1.Text = _HospitalPatient.Remark1.Trim();
                    txtRemark2.Text = _HospitalPatient.Remark2.Trim();
                    txtRemark3.Text = _HospitalPatient.Remark3.Trim();
                    mcbDoctor.SelectedID = _HospitalPatient.DoctorID;
                    mcbWard.SelectedID = _HospitalPatient.WardID;
                    mcbAccount.SelectedID = _HospitalPatient.AccountID;
                    txtAgeYears.Text = _HospitalPatient.Ageyears.ToString();
                    txtAgeMonths.Text = _HospitalPatient.Agemonths.ToString();
                    txtAgeDays.Text = _HospitalPatient.Agedays.ToString();
                    txtIDNumber.Text = _HospitalPatient.IDNumber.ToString();
                    txtInwardNumber.Text = _HospitalPatient.InwardNumber.ToString();
                    txtRoomNumber.Text = _HospitalPatient.RoomNumber.ToString();
                    if (_HospitalPatient.Gender == "F")
                        rbFemale.Checked = true;
                    else
                        rbMale.Checked = true;
                }
                //if (Mode == OperationMode.Delete || Mode == OperationMode.View)

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FillSearchData>>" + Ex.Message);
            }
            return true;
        }
        public void FilltxtInwardNumber()
        {
            try
            {
                txtInwardNumber.SelectedID = null;
                txtInwardNumber.SourceDataString = new string[6] { "ID", "InwardNumber", "PatientName", "Address1", "Telephone", "MaleFemale" };
                txtInwardNumber.ColumnWidth = new string[6] { "0", "70", "150", "150", "100", "30" };
                HospitalPatient _Pat = new HospitalPatient();
                DataTable dtable = _Pat.GetOverviewData();
                txtInwardNumber.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FilltxtName>>" + Ex.Message);
            }
        }
        #endregion IDetail Control

        #region IDetail Members

        public override void ReFillData()
        {
            FilltxtInwardNumber();
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
           
            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                txtAddress1.Focus();
                retValue = true;
            }
             if (keyPressed == Keys.B && modifier == Keys.Alt)
            {
                txtdd.Focus();
                retValue = true;
            }
             if (keyPressed == Keys.D && modifier == Keys.Alt)
             {
                 mcbDoctor.Focus();
                 retValue = true;
             }
            if (keyPressed == Keys.I && modifier == Keys.Alt)
            {
                txtEmailId.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.N && modifier == Keys.Alt)
            {
                txtName.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.R && modifier == Keys.Alt)
            {
                txtRemark1.Focus();
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

        #endregion IDetail Members

        #region Other private methods

        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[5] { "DocID", "DocName", "DocAddress", "DocTelephone", "DocEmailID" };
                mcbDoctor.ColumnWidth = new string[5] { "0", "200", "0", "0", "0" };
                mcbDoctor.ValueColumnNo = 0;
                UclDoctor uclDoc = new UclDoctor();
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _doctor = new Doctor();
                DataTable ddoctortable = _doctor.GetOverviewData();
                mcbDoctor.FillData(ddoctortable);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FillDoctorCombo>>" + Ex.Message);
            }
        }
        private void FillWardCombo()
        {
            try
            {
                mcbWard.SelectedID = null;
                mcbWard.SourceDataString = new string[2] { "WardID", "WardName" };
                mcbWard.ColumnWidth = new string[2] { "0", "200" };
                mcbWard.ValueColumnNo = 0;
                UclWard uclWard = new UclWard();
                mcbWard.UserControlToShow = new UclWard();
                Ward _Ward = new Ward();
                DataTable wardtable = _Ward.GetOverviewData();
                mcbWard.FillData(wardtable);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.FillDoctorCombo>>" + Ex.Message);
            }
        }
        private void FillAccountCombo()
        {
            try
            {
                mcbAccount.SelectedID = null;
                mcbAccount.SourceDataString = new string[4] { "AccountID", "AccCode", "AccName", "AccAddress1" };
                mcbAccount.ColumnWidth = new string[4] { "0", "20", "200", "200" };
                mcbAccount.DisplayColumnNo = 2;
                mcbAccount.ValueColumnNo = 0;
                Account filltxt = new Account();
                DataTable dtable = filltxt.GetOverviewData();
                mcbAccount.FillData(dtable);
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

        private void ClearControls()
        {
            try
            {
                txtName.Text = "";
                txtInwardNumber.SelectedID = "";
                txtInwardNumber.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtTelephone.Clear();
                txtEmailId.Clear();
                txtNameAddress.Clear();
                txtdd.Clear();
                txtmm.Clear();
                txtyy.Clear();
                txtRemark1.Clear();
                txtRemark2.Clear();
                txtRemark3.Clear();
                mcbDoctor.SelectedID = "";
                txtNoOfRows.Text = "";
                txtIDNumber.Text = "";
                txtRoomNumber.Text = "";
                mcbWard.SelectedID = "";
                mcbAccount.SelectedID = "";
                txtAgeDays.Clear();
                txtAgeMonths.Clear();
                txtAgeYears.Clear();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.ClearControls>>" + Ex.Message);
            }
        }

        #endregion

        # region Events

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {                    
                    case Keys.Enter:
                        txtAddress1.Focus();
                        txtNameAddress.Text = txtName.Text.Trim() + " " + txtAddress1.Text.Trim();
                        break;
                    case Keys.Down:
                        txtAddress1.Focus();
                        break;
                    case Keys.Up:
                        txtInwardNumber.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtName_KeyDown>>" + Ex.Message);
            }

        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtEmailId.Focus();
                        break;
                    case Keys.Down:
                        txtEmailId.Focus();
                        break;
                    case Keys.Up:
                        txtAddress2.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtTelephone_KeyDown>>" + Ex.Message);
            }
        }

        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtEmailId_KeyDown>>" + Ex.Message);
            }
        }

        private void txtShortNameAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
                        txtEmailId.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtShortNameAddress_KeyDown>>" + Ex.Message);
            }
        }

        private void txtdd_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
                        txtNameAddress.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtdd_KeyDown>>" + Ex.Message);
            }
        }

        private void txtmm_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtmm_KeyDown>>" + Ex.Message);
            }
        }

        private void txtyy_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {                  
                    case Keys.Up:
                        txtmm.Focus();
                        break;
                    case Keys.Enter:
                        txtAgeYears.Focus();
                        break;
                    case Keys.Down:
                        txtAgeYears.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }
        private void txtAgeYears_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        txtyy.Focus();
                        break;
                    case Keys.Enter:
                        txtAgeMonths.Focus();
                        break;
                    case Keys.Down:
                        txtAgeMonths.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }

        private void txtAgeMonths_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        txtAgeYears.Focus();
                        break;
                    case Keys.Enter:
                        txtAgeDays.Focus();
                        break;
                    case Keys.Down:
                        txtAgeDays.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }
        private void txtAgeDays_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        txtAgeMonths.Focus();
                        break;
                    case Keys.Enter:
                        if (rbFemale.Checked == true)
                            rbFemale.Focus();
                        else
                            rbMale.Focus();
                        break;
                    case Keys.Down:
                        if (rbFemale.Checked == true)
                            rbFemale.Focus();
                        else
                            rbMale.Focus();
                        break;
                        
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }

        private void rbMale_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        txtAgeDays.Focus();
                        break;
                    case Keys.Enter:
                        txtRoomNumber.Focus();
                        break;
                    case Keys.Down:
                        txtRoomNumber.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }

        private void rbFemale_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {                    
                    case Keys.Enter:
                        txtRoomNumber.Focus();
                        break;
                    case Keys.Down:
                        txtRoomNumber.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }
    
      
        private void txtRoomNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        rbMale.Focus();
                        break;
                    case Keys.Enter:
                        mcbWard.Focus();                       
                        break;
                    case Keys.Down:
                        mcbWard.Focus();         
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }

     

        private void mcbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        mcbAccount.Focus();
                        e.Handled = true;
                        break;
                    case Keys.Down:
                        mcbAccount.Focus();
                        e.Handled = true;
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_KeyDown>>" + Ex.Message);
            }
        }
        private void mcbAccount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        mcbDoctor.Focus();
                        e.Handled = true;
                        break;
                    case Keys.Enter:
                        txtIDNumber.Focus();
                        e.Handled = true;
                        break;
                    case Keys.Down:
                        txtIDNumber.Focus();
                        e.Handled = true;
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_KeyDown>>" + Ex.Message);
            }
        }
        private void txtIDNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        mcbAccount.Focus();
                        e.Handled = true;
                        break;
                    case Keys.Enter:
                        txtRemark1.Focus();
                        break;
                    case Keys.Down:
                        txtRemark1.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.mcbDoctor_KeyDown>>" + Ex.Message);
            }
        }

        private void txtRemark1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        txtIDNumber.Focus();
                        break;
                    case Keys.Enter:
                        txtRemark2.Focus();
                        break;
                    case Keys.Down:
                        txtRemark2.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }

        private void txtRemark2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        txtRemark1.Focus();
                        break;
                    case Keys.Enter:
                        txtRemark3.Focus();
                        break;
                    case Keys.Down:
                        txtRemark3.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }

        }

        private void txtRemark3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        txtRemark2.Focus();
                        break;
                    case Keys.Enter:
                        txtRemark3.Focus();
                        break;
                    case Keys.Down:
                        txtRemark3.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtyy_KeyDown>>" + Ex.Message);
            }
        }


        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtName_TextChanged>>" + Ex.Message);
            }
        }

        private void headerLabel1_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void txtInwardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtName.Focus();
        }
        private void txtInwardNumber_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                FillSearchData(txtInwardNumber.SelectedID,"");
                if (_Mode == OperationMode.Add)
                {
                    txtInwardNumber.SelectedID = "";
                    _HospitalPatient.Id = "";
                }
                txtName.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtName_EnterKeyPressed>>" + Ex.Message);
            }
        }
        private void mcbWard_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();
        }

        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbAccount.Focus();
        }

        private void mcbAccount_EnterKeyPressed(object sender, EventArgs e)
        {
            txtIDNumber.Focus();
        }
        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        txtAddress2.Focus();
                        break;
                    case Keys.Enter:
                        txtAddress2.Focus();
                        break;
                    case Keys.Up:
                        txtName.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtAddress1_KeyDown>>" + Ex.Message);
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        txtTelephone.Focus();
                        break;
                    case Keys.Enter:
                        txtTelephone.Focus();
                        break;
                    case Keys.Up:
                        txtAddress1.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.txtAddress2_KeyDown>>" + Ex.Message);
            }
        }

        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
        }
        private void mcbWard_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbWard.SelectedID;
                FillWardCombo();
                mcbWard.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbDoctor.SelectedID;
                FillDoctorCombo();
                mcbDoctor.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbAccount_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbAccount.SelectedID;
                FillAccountCombo();
                mcbAccount.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        # endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(txtName, "use A-Z,0-9,space only");
                ttToolTip.SetToolTip(mcbAccount, "If Payment will be done by Debtor. The Patient is admitted through any Company,Institution etc.");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion  ToolTip       
             
    }
}
