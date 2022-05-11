using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSRetailPlus.Common;
using System.Text.RegularExpressions;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclAccount : BaseControl
    {
        #region Declaration
        private Account _Account;
        #endregion

        #region  Constructor
        public UclAccount()
        {
            try
            {
                InitializeComponent();
                _Account = new Account();
                SearchControl = new UclAccountSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _Account.Initialise();
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
                txtName.Text = "";
                txtName.SelectedID = "";
                FilltxtName();
                headerLabel1.Text = "ACCOUNT -> NEW";
                HidePanels();
                FillGroupCombo();
                FillBankCombo();
                FillBranchCombo();
                FillDoctorCombo();
                GetAccTokenNumber();
                rbtnCreditorDetails.Checked = false;
                txtName.Focus();
                AddToolTip();
                gbAccountType.Enabled = true;

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
                txtName.Text = "";
                txtName.SelectedID = "";
                headerLabel1.Text = "ACCOUNT -> EDIT";
                AddToolTip();
                FilltxtName();
                HidePanels();
                FillGroupCombo();
                FillBankCombo();
                FillBranchCombo();
                FillDoctorCombo();
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
            ClearControls();
            pnlGeneral.Visible = false;
            pnlDebtor.Visible = false;
            pnlCreditor.Visible = false;
            pnlBank.Visible = false;
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "ACCOUNT -> DELETE";
                ClearData();
                txtName.Text = "";
                txtName.SelectedID = "";
                HidePanels();
                FilltxtName();
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
            bool retValue = false;
            try
            {
                _Account.Id = txtName.SelectedID;
                if (_Account.Id != null && _Account.Id != "")
                {
                    retValue = _Account.CanBeDeleted();
                    if (retValue == true)
                    {
                        retValue = _Account.DeleteDetails();
                        MessageBox.Show("Account information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                ClearData();
                txtName.Text = "";
                txtName.SelectedID = "";
                FilltxtName();
                HidePanels();
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
                txtName.Text = "";
                txtName.SelectedID = "";
                FilltxtName();
                HidePanels();
                headerLabel1.Text = "ACCOUNT -> VIEW";
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
                string VisitDaysForDB = "";
                _Account.Name = txtName.Text.Trim().ToString();
                _Account.AccName = _Account.Name;
                _Account.AccGroupID = mcbGroup.SelectedID;
                if (cbSetAsDefault.Checked == true)
                    _Account.SetAsDefault = "Y";
                else
                    _Account.SetAsDefault = "N";
                if (txtOpeningDebit.Text != null && txtOpeningDebit.Text != "")
                    _Account.AccOpeningDebit = Convert.ToDouble(txtOpeningDebit.Text.ToString());
                if (txtOpeningCredit.Text != null && txtOpeningCredit.Text != "")
                    _Account.AccOpeningCredit = Convert.ToDouble(txtOpeningCredit.Text.ToString());
                if (txtAddress1.Text != null)
                    _Account.AccAddress1 = txtAddress1.Text.ToString().Trim();
                if (txtAddress2.Text != null)
                    _Account.AccAddress2 = txtAddress2.Text.ToString().Trim();
                if (txtContactPerson.Text != null && txtContactPerson.Text != "")
                    _Account.AccContactPerson = txtContactPerson.Text.ToString().Trim();
                if (txtTelephone.Text != null && txtTelephone.Text != "")
                    _Account.AccTelephone = txtTelephone.Text.ToString().Trim();
                if (txtEmailId.Text != null && txtEmailId.Text != "")
                    _Account.AccEmailID = txtEmailId.Text.ToString().Trim();
                if (txtRemark1.Text != null && txtRemark1.Text != "")
                    _Account.AccRemark1 = txtRemark1.Text.ToString().Trim();
                if (txtRemark2.Text != null && txtRemark2.Text != "")
                    _Account.AccRemark2 = txtRemark2.Text.ToString().Trim();
                if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                {
                    rbtnDebtorDetails.Checked = true;
                    if (txtNameAddress.Text != null)
                        _Account.AccNameAddress = txtNameAddress.Text.Trim();
                    if (_Account.AccNameAddress.Length > 50)
                        _Account.AccNameAddress = _Account.AccNameAddress.Substring(0, 50);
                    if (mcbBank.SelectedID != null)
                        _Account.AccBankId = mcbBank.SelectedID;
                    if (mcbBranch.SelectedID != null)
                        _Account.AccBranchID = mcbBranch.SelectedID;
                    if (mcbDoctor.SelectedID != null)
                        _Account.AccDoctorID = mcbDoctor.SelectedID;

                    if (txtvisit1.Text != null && txtvisit1.Text != "")
                        _Account.AccDbVisitDay1 = Convert.ToInt32(txtvisit1.Text.ToString());
                    if (txtvisit2.Text != null && txtvisit2.Text != "")
                        _Account.AccDbVisitDay2 = Convert.ToInt32(txtvisit2.Text.ToString());
                    if (txtvisit3.Text != null && txtvisit3.Text != "")
                        _Account.AccDbVisitDay3 = Convert.ToInt32(txtvisit3.Text.ToString());




                    if (txtVATTIN.Text != null)
                        _Account.AccVATTinNumber = txtVATTIN.Text.Trim();
                    if (txtDLN.Text != null)
                        _Account.AccDLN = txtDLN.Text.Trim();
                    if (txtTokenNumber.Text != null && txtTokenNumber.Text != "")
                        _Account.AccTokenNumber = Convert.ToInt32(txtTokenNumber.Text.ToString().Trim());
                    if (rbCash.Checked == true)
                        _Account.AccTransactionType = "CS";
                    else if (rbCredit.Checked == true)
                        _Account.AccTransactionType = "CR";
                    else
                        _Account.AccTransactionType = "CC";
                    if (txtDebtorDiscountOffered.Text != null && txtDebtorDiscountOffered.Text != "")
                    _Account.AccDiscountOffered = Convert.ToDouble(txtDebtorDiscountOffered.Text.ToString());
                    
                }
                else if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                {
                    rbtnCreditorDetails.Checked = true;
                    if (txtCreditorShortName.Text != null)
                        _Account.AccShortName = txtCreditorShortName.Text.ToString().Trim();
                    if (txtCreditorDiscountOffered.Text != null && txtCreditorDiscountOffered.Text != "")
                        _Account.AccDiscountOffered = Convert.ToDouble(txtCreditorDiscountOffered.Text.ToString());
                    if (txtLessPercentInDebitNote.Text != null && txtLessPercentInDebitNote.Text != "")
                        _Account.AccLessPercentInDebitNote = Convert.ToDouble(txtLessPercentInDebitNote.Text.ToString());

                    if (txtCreditorVATTIN.Text != null)
                        _Account.AccVATTinNumber = txtCreditorVATTIN.Text.ToString().Trim();
                    if (txtCreditorDLN.Text != null)
                        _Account.AccDLN = txtCreditorDLN.Text.ToString().Trim();                   
                    if (lvVisitDays.Items[0].Checked)
                        VisitDaysForDB = "0";
                    if (lvVisitDays.Items[1].Checked)
                        VisitDaysForDB = VisitDaysForDB + "1";
                    if (lvVisitDays.Items[2].Checked)
                        VisitDaysForDB = VisitDaysForDB + "2";
                    if (lvVisitDays.Items[3].Checked)
                        VisitDaysForDB = VisitDaysForDB + "3";
                    if (lvVisitDays.Items[4].Checked)
                        VisitDaysForDB = VisitDaysForDB + "4";
                    if (lvVisitDays.Items[5].Checked)
                        VisitDaysForDB = VisitDaysForDB + "5";
                    if (lvVisitDays.Items[6].Checked)
                        VisitDaysForDB = VisitDaysForDB + "6";
                    VisitDaysForDB = VisitDaysForDB.Trim();
                    _Account.AccCrVisitDays = VisitDaysForDB;
                    if (txtStatement15Days.Text != null)
                        _Account.AccStatement15Days = txtStatement15Days.Text.ToString();
                }
                else if (_Account.AccCode == FixAccounts.AccCodeForBank)
                {
                    rbtnBankDetails.Checked = true;
                    if (txtBankAccountNumber != null)
                        _Account.AccBankAccountNumber = txtBankAccountNumber.Text.ToString().Trim();
                }
                else
                {
                    if (txtGeneralPAN.Text != null)
                        _Account.AccPAN = txtGeneralPAN.Text.ToString().Trim();
                    if (txtGeneralVATTIN.Text != null)
                        _Account.AccVATTinNumber = txtGeneralVATTIN.Text.ToString().Trim();
                }
                if (_Mode == OperationMode.Edit)
                    _Account.IFEdit = "Y";
                _Account.Validate();
                if (_Account.IsValid)
                {
                    //LockTable.LockTableForAccount();
                   
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _Account.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Account.CreatedBy = General.CurrentUser.Id;
                        _Account.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Account.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                        {
                            if (_Account.AccTokenNumber > 0)
                            {
                               retValue = _Account.UpdateTokenNumber();

                            }                         
                            retValue = _Account.AddDebtorDetails();
                        }
                        else if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                            retValue = _Account.AddCreditorDetails();
                        else if (_Account.AccCode == FixAccounts.AccCodeForBank)
                        {
                            retValue = _Account.AddBankDetails();
                            if (_Account.SetAsDefault == "Y")
                            {
                                _Account.ClearAllSetDefault();
                                _Account.SetThisAsDefault(_Account.Id);
                            }                       
                            
                        }

                        else
                            retValue = _Account.AddDetailsGnrl();
                        if (retValue)
                        {
                            MessageBox.Show("Account information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _Account.Id;
                            ClearControls();
                        }
                        else
                            MessageBox.Show("Error While Saving", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return retValue;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _Account.ModifiedBy = General.CurrentUser.Id;
                        _Account.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Account.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                            retValue = _Account.UpdateDebtorDetails();
                        else if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                            retValue = _Account.UpdateCreditorDetails();
                        else if (_Account.AccCode == FixAccounts.AccCodeForGeneral)
                            retValue = _Account.UpdateDetailsGnrl();
                        else
                        {
                            retValue = _Account.UpdateBankDetails();
                            if (_Account.SetAsDefault == "Y")
                            {
                                _Account.ClearAllSetDefault();
                                _Account.SetThisAsDefault(_Account.Id);
                            }        
                        }
                        if (retValue)
                        {
                            MessageBox.Show("Account information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _Account.Id;
                        }
                        else
                        {
                            MessageBox.Show("CAN NOT SAVE.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _SavedID = _Account.Id;

                        }
                    }
                }
                else // Show Validation Messages
                {
                  //  LockTable.UnLockTables();
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Account.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
             //   LockTable.UnLockTables();
                Log.WriteException(Ex);              
            }
        //    LockTable.UnLockTables();
            return retValue;
        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _Account.Id = ID;
                    _Account.ReadDetailsByID();
                    FillBankCombo();
                    FillBranchCombo();
                    FillGroupCombo();
                    FillDoctorCombo();
                  
                    //For Account type
                    if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                    {
                        rbtnDebtorDetails.Checked = true;
                        mcbGroup.Enabled = false;
                        pnlDebtor.Visible = true;
                    }
                    if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                    {
                        rbtnCreditorDetails.Checked = true;
                        mcbGroup.Enabled = false;
                        pnlCreditor.Visible = true;
                    }
                    if (_Account.AccCode == FixAccounts.AccCodeForBank)
                    {
                        rbtnBankDetails.Checked = true;
                        mcbGroup.Enabled = false;
                        pnlBank.Visible = true;
                        if (_Account.SetAsDefault == "Y")
                            cbSetAsDefault.Checked = true;
                        else
                            cbSetAsDefault.Checked = false;

                    }
                    if (_Account.AccCode != FixAccounts.AccCodeForDebtor && _Account.AccCode != FixAccounts.AccCodeForCreditor && _Account.AccCode != FixAccounts.AccCodeForBank)
                    {
                        rbtnGeneralDetails.Checked = true;
                        pnlGeneral.Visible = true;
                    }
                    txtName.Text = _Account.AccName;
                    mcbGroup.SelectedID = _Account.AccGroupID;
                    if (_Account.AccOpeningCredit != 0)
                        txtOpeningCredit.Text = _Account.AccOpeningCredit.ToString("#0.00");
                    else
                        txtOpeningCredit.Text = "";
                    if (_Account.AccOpeningDebit != 0)
                        txtOpeningDebit.Text = _Account.AccOpeningDebit.ToString("#0.00");
                    else
                        txtOpeningDebit.Text = "";
                    if (_Account.AccAddress1 != null)
                        txtAddress1.Text = _Account.AccAddress1.Trim();
                    if (_Account.AccAddress2 != null)
                        txtAddress2.Text = _Account.AccAddress2.Trim();
                    if (_Account.AccContactPerson != null)
                        txtContactPerson.Text = _Account.AccContactPerson.Trim();
                    if (_Account.AccTelephone != null)
                        txtTelephone.Text = _Account.AccTelephone.Trim();
                    if (_Account.AccEmailID != null)
                        txtEmailId.Text = _Account.AccEmailID.Trim();
                    if (_Account.AccRemark1 != null)
                        txtRemark1.Text = _Account.AccRemark1.Trim();
                    if (_Account.AccRemark2 != null)
                        txtRemark2.Text = _Account.AccRemark2.Trim();
                    if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                    {
                        if (_Account.AccNameAddress != null)
                            txtNameAddress.Text = _Account.AccNameAddress.Trim();
                        if (_Account.AccBankId != null)
                            mcbBank.SelectedID = _Account.AccBankId.Trim();
                        if (_Account.AccBranchID != null)
                            mcbBranch.SelectedID = _Account.AccBranchID.Trim();
                        if (_Account.AccDoctorID != null)
                            mcbDoctor.SelectedID = _Account.AccDoctorID.Trim();
                        if (_Account.AccDbVisitDay1 != 0)
                            txtvisit1.Text = _Account.AccDbVisitDay1.ToString();
                        if (_Account.AccDbVisitDay2 != 0)
                            txtvisit2.Text = _Account.AccDbVisitDay2.ToString();
                        if (_Account.AccDbVisitDay3 != 0)
                            txtvisit3.Text = _Account.AccDbVisitDay3.ToString();

                        if (_Account.AccVATTinNumber != null)
                            txtVATTIN.Text = _Account.AccVATTinNumber.Trim();
                        if (_Account.AccDLN != null)
                            txtDLN.Text = _Account.AccDLN.Trim();
                        //if (_Account.AccTokenNumber != null)
                        txtTokenNumber.Text = _Account.AccTokenNumber.ToString().Trim();
                        if (_Account.AccTransactionType == "CS")
                            rbCash.Checked = true;
                        else if (_Account.AccTransactionType == "CR")
                            rbCredit.Checked = true;
                        else
                            rbCashCredit.Checked = true;
                    }
                    else if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                    {
                        if (_Account.AccShortName != null)
                            txtCreditorShortName.Text = _Account.AccShortName.Trim();
                        txtCreditorDiscountOffered.Text = _Account.AccDiscountOffered.ToString("#0.00");
                        txtLessPercentInDebitNote.Text = _Account.AccLessPercentInDebitNote.ToString("#0.00");
                        if (_Account.AccVATTinNumber != null)
                            txtCreditorVATTIN.Text = _Account.AccVATTinNumber.Trim();
                        if (_Account.AccDLN != null)
                            txtCreditorDLN.Text = _Account.AccDLN.Trim();
                        if (_Account.AccStatement15Days != null)
                            txtStatement15Days.Text = _Account.AccStatement15Days;
                        if (_Account.AccCrVisitDays.Contains('0'))
                            lvVisitDays.Items[0].Checked = true;
                        if (_Account.AccCrVisitDays.Contains('1'))
                            lvVisitDays.Items[1].Checked = true;
                        if (_Account.AccCrVisitDays.Contains('2'))
                            lvVisitDays.Items[2].Checked = true;
                        if (_Account.AccCrVisitDays.Contains('3'))
                            lvVisitDays.Items[3].Checked = true;
                        if (_Account.AccCrVisitDays.Contains('4'))
                            lvVisitDays.Items[4].Checked = true;
                        if (_Account.AccCrVisitDays.Contains('5'))
                            lvVisitDays.Items[5].Checked = true;
                        if (_Account.AccCrVisitDays.Contains('6'))
                            lvVisitDays.Items[6].Checked = true;
                    }
                    else if (_Account.AccCode == FixAccounts.AccCodeForBank)
                        txtBankAccountNumber.Text = _Account.AccBankAccountNumber.Trim();
                    else
                    {
                        if (_Account.AccVATTinNumber != null)
                            txtGeneralVATTIN.Text = _Account.AccVATTinNumber.Trim();
                        if (_Account.AccPAN != null)
                            txtGeneralPAN.Text = _Account.AccPAN.Trim();
                        FillGroupCombo();
                        mcbGroup.SelectedID = _Account.AccGroupID;
                        ShowHidePanels();
                    }

                    bool canacccodechange = _Account.CanBeDeleted();
                    if (!canacccodechange)
                    {
                        gbAccountType.Enabled = false;
                    }
                    if (_Mode == OperationMode.View || _Mode == OperationMode.Delete)
                        pnlCenter.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }



        # endregion Idetail Control

        #region IDetail Members

        public override void ReFillData()
        {
            try
            {
                FillBankCombo();
                FillBranchCombo();
                FillGroupCombo();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    txtName.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.G && modifier == Keys.Alt)
                {
                    mcbGroup.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.B && modifier == Keys.Alt)
                {
                    txtOpeningDebit.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    txtOpeningCredit.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    txtAddress1.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    txtContactPerson.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    txtTelephone.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.I && modifier == Keys.Alt)
                {
                    txtEmailId.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.R && modifier == Keys.Alt)
                {
                    txtRemark1.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                        txtCreditorShortName.Focus();
                    else if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                        txtNameAddress.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    txtCreditorDiscountOffered.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.V && modifier == Keys.Alt)
                {
                    if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                        txtCreditorVATTIN.Focus();
                    else if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                        txtVATTIN.Focus();
                    else if (_Account.AccCode != FixAccounts.AccCodeForBank)
                        txtGeneralVATTIN.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                    if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                        txtCreditorDLN.Focus();
                    else if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                        txtDLN.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Y && modifier == Keys.Alt)
                {
                    if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                        lvVisitDays.Focus();
                    else if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                        rbCash.Focus();
                    retValue = true;

                }
                if (keyPressed == Keys.K && modifier == Keys.Alt)
                {
                    mcbBank.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.H && modifier == Keys.Alt)
                {
                    mcbBranch.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.E && modifier == Keys.Alt)
                {
                    txtTokenNumber.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.U && modifier == Keys.Alt)
                {
                    txtAccountNumber.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    txtGeneralPAN.Focus();
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
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public void HidePanels()
        {
            try
            {
                pnlBank.Visible = false;
                pnlCreditor.Visible = false;
                pnlDebtor.Visible = false;
                pnlGeneral.Visible = false;
                pnlCenter.Enabled = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion Idetail Members

        #region Other private methods
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
                txtCreditorDiscountOffered.Clear();
                txtBankAccountNumber.Clear();
              //  txtName.Text = "";
             //   txtName.SelectedID = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtContactPerson.Clear();
                txtEmailId.Clear();
                mcbGroup.SelectedID = "";
                //   txtGeneralGroupName.Clear();
                txtOpeningCredit.Clear();
                txtOpeningDebit.Clear();
                txtRemark1.Text = "";
                txtRemark2.Text = "";
                txtTelephone.Clear();
                rbtnCreditorDetails.Enabled = true;
                rbtnDebtorDetails.Enabled = true;
                rbtnBankDetails.Enabled = true;
                rbtnGeneralDetails.Enabled = true;
                rbtnSaleDetails.Enabled = true;
                rbtnPurchaseDetails.Enabled = true;
                rbtnOCreditorDetails.Enabled = true;
                rbtnODebtorDetails.Enabled = true;
                rbtnCreditorDetails.Checked = false;
                rbtnDebtorDetails.Checked = false;
                rbtnBankDetails.Checked = false;
                rbtnGeneralDetails.Checked = false;
                rbtnSaleDetails.Checked = false;
                rbtnPurchaseDetails.Checked = false;
                rbtnOCreditorDetails.Checked = false;
                rbtnODebtorDetails.Checked = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void GetAccountType()
        {
            try
            {
                if (_Mode == OperationMode.Edit)
                {
                    mcbGroup.Enabled = true;
                    mcbGroup.SelectedID = "";
                    FillGroupCombo();
                }
                if (rbtnDebtorDetails.Checked == true)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForDebtor;
                    _Account.AccGroupID = FixAccounts.GroupCodeForDebtor;
                    FillGroupCombo();
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForDebtor;
                    mcbGroup.Enabled = false;
                    txtOpeningDebit.Focus();
                }
                else if (rbtnCreditorDetails.Checked == true)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForCreditor;
                    _Account.AccGroupID = FixAccounts.GroupCodeForCreditor;
                    FillGroupCombo();
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForCreditor;
                    mcbGroup.Enabled = false;                 
                }
                else if (rbtnBankDetails.Checked == true)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForBank;
                    _Account.AccGroupID = FixAccounts.GroupCodeForBank;
                    FillGroupCombo();
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForBank;
                    mcbGroup.Enabled = false;
                    txtOpeningDebit.Focus();
                }
                else if (rbtnGeneralDetails.Checked == true)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForGeneral;
                    _Account.AccGroupID = "";
                    mcbGroup.SelectedID = "";                   
                    FillGroupCombo();                  
                    mcbGroup.Enabled = true;
                    mcbGroup.Focus();
                }
                else if (rbtnSaleDetails.Checked == true)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForSale;
                    mcbGroup.SelectedID = null;         
                    FillGroupCombo();
                    mcbGroup.Enabled = true;
                    mcbGroup.Focus();
                }
                else if (rbtnPurchaseDetails.Checked == true)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForPurchase;
                    mcbGroup.SelectedID = null;         
                    FillGroupCombo();
                    mcbGroup.Enabled = true;
                    mcbGroup.Enabled = true;

                }
                else if (rbtnOCreditorDetails.Checked == true)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForOtherCreditor;
                    _Account.AccGroupID = FixAccounts.GroupCodeForOtherCreditor;
                    mcbGroup.SelectedID = null;         
                    FillGroupCombo();
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForOtherCreditor;
                    mcbGroup.Enabled = false;
                    txtOpeningDebit.Focus();
                }
                else
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForOtherDebtor;
                    _Account.AccGroupID = FixAccounts.GroupCodeForOtherDebtor;
                    mcbGroup.SelectedID = null;         
                    FillGroupCombo();
                    mcbGroup.SelectedID = null;         
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForOtherDebtor;
                    mcbGroup.Enabled = false;
                    txtOpeningDebit.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ShowHidePanels()
        {
            try
            {
                txtName.Focus();
                if (rbtnDebtorDetails.Checked)
                {
                    pnlCreditor.Visible = false;
                    pnlBank.Visible = false;
                    pnlGeneral.Visible = false;
                    pnlDebtor.Visible = true;
                    pnlDebtor.BringToFront();
                }
                else if (rbtnCreditorDetails.Checked)
                {
                    pnlDebtor.Visible = false;
                    pnlBank.Visible = false;
                    pnlGeneral.Visible = false;
                    pnlCreditor.Visible = true;
                    pnlCreditor.BringToFront();
                    mcbGroup.SelectedID = "31";
                    FillGroupCombo();
                }
                else if (rbtnBankDetails.Checked)
                {
                    pnlDebtor.Visible = false;
                    pnlCreditor.Visible = false;
                    pnlGeneral.Visible = false;
                    pnlBank.Visible = true;
                    pnlBank.BringToFront();
                }
                else if (rbtnGeneralDetails.Checked || rbtnOCreditorDetails.Checked || rbtnODebtorDetails.Checked || rbtnPurchaseDetails.Checked || rbtnSaleDetails.Checked)
                {
                    pnlDebtor.Visible = false;
                    pnlCreditor.Visible = false;
                    pnlBank.Visible = false;
                    pnlGeneral.Visible = true;
                    pnlGeneral.BringToFront();                    
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        //private void DisplayVisitDaysGB()
        //{
        //    gbVisitDays.Visible = true;
        //    //gbVisitDays.BringToFront();
        //    Point point = new Point(405, 353);
        //    gbVisitDays.Location = point;
        //    lvVisitDays.Focus();
        //}
        private bool ValidateEmail(TextBox EmailTextBoxName)
        {
            try
            {
                System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                if (EmailTextBoxName.Text.Trim().Length > 0)
                {
                    if (!rEMail.IsMatch(EmailTextBoxName.Text.Trim()))
                    {
                        MessageBox.Show("E-Mail expected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        EmailTextBoxName.SelectAll();
                        return true;
                        //e.Cancel = true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return false;
        }
        # region Fillcombo

        private void FillGroupCombo()
        {
            try
            {
                mcbGroup.SelectedID = null;
                mcbGroup.SourceDataString = new string[3] { "GroupID", "GroupCode", "GroupName" };
                mcbGroup.ColumnWidth = new string[3] { "0", "20", "200" };
                mcbGroup.DisplayColumnNo = 2;
                mcbGroup.ValueColumnNo = 0;
                mcbGroup.UserControlToShow = new UclGroup();
                Groupac _Group = new Groupac();
                DataTable dgrouptable = null;

                if (_Account.AccCode == FixAccounts.AccCodeForDebtor || _Account.AccCode == FixAccounts.AccCodeForCreditor || _Account.AccCode == FixAccounts.AccCodeForBank || _Account.AccCode == FixAccounts.AccCodeForOtherCreditor || _Account.AccCode == FixAccounts.AccCodeForOtherDebtor)
                    dgrouptable = _Group.GetOverviewDataForFixedCode(_Account.AccGroupID);
                else
                {
                    dgrouptable = _Group.GetOverviewDataForGeneral();
                }
                mcbGroup.FillData(dgrouptable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillAccountType()
        {
            cbAccountType.Items.Clear();

            cbAccountType.Items.Add(FixAccounts.AccTypeForCreditor);
            cbAccountType.Items.Add(FixAccounts.AccTypeForDebtor);
            cbAccountType.Items.Add(FixAccounts.AccTypeForBank);
            cbAccountType.Items.Add(FixAccounts.AccTypeForGeneral);
            cbAccountType.Items.Add(FixAccounts.AccTypeForOtherCreditor);
            cbAccountType.Items.Add(FixAccounts.AccTypeForOtherDebtor);
            cbAccountType.Items.Add(FixAccounts.AccTypeForPurchase);
            cbAccountType.Items.Add(FixAccounts.AccTypeForSale);

            cbAccountType.Text = FixAccounts.AccTypeForCreditor;
            cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
        }
        public void FilltxtName()
        {
            try
            {
                txtName.SelectedID = null;
                txtName.SourceDataString = new string[4] { "AccountID", "AccCode", "AccName", "AccAddress1" };
                txtName.ColumnWidth = new string[4] { "0", "20", "200", "200" };
                txtName.DisplayColumnNo = 2;
                txtName.ValueColumnNo = 0;
                Account filltxt = new Account();
                DataTable dtable = filltxt.GetOverviewData();
                txtName.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillBankCombo()
        {
            try
            {
                mcbBank.SelectedID = null;
                mcbBank.SourceDataString = new string[2] { "BankID", "BankName" };
                mcbBank.ColumnWidth = new string[2] { "0", "200" };
                mcbBank.ValueColumnNo = 0;
                mcbBank.UserControlToShow = new UclBank();
                Bank _Bank = new Bank();
                DataTable dbanktable = _Bank.GetOverviewData();
                mcbBank.FillData(dbanktable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillBranchCombo()
        {
            try
            {
                mcbBranch.SelectedID = null;
                mcbBranch.SourceDataString = new string[2] { "BranchId", "BranchName" };
                mcbBranch.ColumnWidth = new string[2] { "0", "200" };
                mcbBranch.ValueColumnNo = 0;
                mcbBranch.UserControlToShow = new UclBranch();
                Branch _Branch = new Branch();
                DataTable dbranchtable = _Branch.GetOverviewData();
                mcbBranch.FillData(dbranchtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[4] { "DocID", "DocName", "DocAddress", "DocShortNameAddress" };
                mcbDoctor.ColumnWidth = new string[4] { "0", "150", "150", "0" };
                mcbDoctor.ValueColumnNo = 0;
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _Doctor = new Doctor();
                DataTable dbranchtable = _Doctor.GetOverviewData();
                mcbDoctor.FillData(dbranchtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void GetAccTokenNumber()
        {
            try
            {
                _Account.GetCurrentTokenNumber();
                txtTokenNumber.Text = _Account.CurrentTokenNumber.ToString();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        #endregion OtherPrivate Methods

        #region Events
        #region checkedchange

        //private void rbtnCreditorDetails_Click(object sender, EventArgs e)
        //{
        //    if (rbtnCreditorDetails.Checked == true)
        //    rbtnClick();
        //}

        public void rbtnClick()
        {
            try
            {
                ShowHidePanels();
                GetAccountType();
                if (mcbGroup.Enabled == true)
                    mcbGroup.Focus();
                else
                    txtOpeningDebit.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void rbtnDebtorDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnClick();
        }
        private void rbtnBankDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnClick();
        }
        private void rbtnGeneralDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnClick();
        }
        private void rbtnSaleDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnClick();
        }
        private void rbtnPurchaseDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnClick();
        }
        private void rbtnOCreditorDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnClick();
        }
        private void rbtnODebtorDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnClick();
        }







        #endregion
        private void txtDebtorVisitDay1_Validating(object sender, CancelEventArgs e)
        {
            //if (txtDebtorVisitDay1.Text.Trim() != "")
            //{
            //    int numberEntered = int.Parse(txtDebtorVisitDay1.Text.Trim());
            //    if (numberEntered < 1 || numberEntered > 31)
            //    {
            //        e.Cancel = true;
            //        MessageBox.Show("Not valid entry", "PharmaSYS+Gold", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
        }
        private void txtDebtorVisitDay2_Validating(object sender, CancelEventArgs e)
        {
            //if (txtDebtorVisitDay2.Text.Trim() != "")
            //{
            //    int numberEntered = int.Parse(txtDebtorVisitDay2.Text.Trim());
            //    if (numberEntered < 1 || numberEntered > 31)
            //    {
            //        e.Cancel = true;
            //        MessageBox.Show("Not valid entry", "PharmaSYS+Gold", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
        }
        private void txtDebtorVisitDay3_Validating(object sender, CancelEventArgs e)
        {
            //if (txtDebtorVisitDay3.Text.Trim() != "")
            //{
            //    int numberEntered = int.Parse(txtDebtorVisitDay3.Text.Trim());
            //    if (numberEntered < 1 || numberEntered > 31)
            //    {
            //        e.Cancel = true;
            //        MessageBox.Show("Not valid entry", "PharmaSYS+Gold", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
        }
        private void txtEmailId_Validating(object sender, CancelEventArgs e)
        {
            if (ValidateEmail(txtEmailId))
                e.Cancel = true;
        }
        private void mcbBank_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbBank.SelectedID;
                FillBankCombo();
                mcbBank.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbBranch_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbBranch.SelectedID;
                FillBranchCombo();
                mcbBranch.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbGroup_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbGroup.SelectedID;
                FillGroupCombo();
                mcbGroup.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbDoctor.SelectedID;
            FillDoctorCombo();
            mcbDoctor.SelectedID = selectedId;
        }
        //private void txtAccountName_TextChanged(object sender, EventArgs e)
        //{
        //    txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
        //}
        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtTokenNumber_Validating(object sender, CancelEventArgs e)
        {
            //try
            //{
            //    if (txtTokenNumber.Text != null && Convert.ToInt32(txtTokenNumber.Text.ToString()) != 0)
            //    {
            //        if (Convert.ToInt32(txtTokenNumber.Text.ToString()) != _Account.CurrentTokenNumber)
            //        {
            //            // lblMessage.Text = "Check Token Number";
            //            txtTokenNumber.Text = _Account.CurrentTokenNumber.ToString("#0");
            //            txtTokenNumber.Focus();
            //        }
            //        else
            //            lblMessage.Text = "";
            //    }
            //    else
            //        lblMessage.Text = "";
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
        }
        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtvisit1.Focus();
        }
        private void txtNameAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        mcbBank.Focus();
                        break;
                    case Keys.Up:
                        txtRemark2.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        //private void rbtnDebtorDetails_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rbtnDebtorDetails.Checked == true)
        //            rbtnClick();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        //private void rbtnBankDetails_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rbtnBankDetails.Checked == true)
        //            rbtnClick();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        //private void rbtnGeneralDetails_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rbtnGeneralDetails.Checked == true)
        //            rbtnClick();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        //private void rbtnSaleDetails_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rbtnSaleDetails.Checked == true)
        //            rbtnClick();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        //private void rbtnPurchaseDetails_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rbtnPurchaseDetails.Checked == true)
        //            rbtnClick();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        //private void rbtnOCreditorDetails_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rbtnOCreditorDetails.Checked == true)
        //            rbtnClick();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        //private void rbtnODebtorDetails_Click(object sender, EventArgs e)
        //{
        //    if (rbtnODebtorDetails.Checked == true)
        //        rbtnClick();
        //}
        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
                    txtAddress2.Focus();
                    break;
                case Keys.Down:
                    txtAddress2.Focus();
                    break;
                case Keys.Up:
                    txtOpeningCredit.Focus();
                    break;
            }
        }
        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
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
                    txtAddress1.Focus();
                    break;
            }
        }
        //private void rbtnCreditorDetails_CheckedChanged(object sender, EventArgs e)
        //{
        //    rbtnClick();
        //}

        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                pnlCenter.Enabled = true;
                string myID = txtName.SelectedID;
                ClearData();
                txtName.SelectedID = myID;               
                if (_Account.AccName != "" && _Account.AccName != txtName.Text.ToString())
                {
                    txtName.SelectedID = "";
                }
                FillSearchData(txtName.SelectedID,"");
                txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAddress1.Text.ToString().Trim();
                if (mcbGroup.SelectedID == null || mcbGroup.SelectedID == "")
                {
                    rbtnCreditorDetails.Focus();
                    rbtnCreditorDetails.Checked = true;
                }
                else
                    if (_Mode == OperationMode.Edit)
                        txtOpeningDebit.Focus();
                    else
                        txtName.Focus();

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtStatement15Days_Validating(object sender, CancelEventArgs e)
        {
            if (txtStatement15Days.Text == " ")
                txtStatement15Days.Text = "Y";
            if (txtStatement15Days.Text != "Y" && txtStatement15Days.Text != "N")
            {
                lblMessage.Text = "Y/N";
                txtStatement15Days.Focus();
            }

            else
            {
                lblMessage.Text = "";
                lvVisitDays.Focus();
            }

        }

        private void rbtnCreditorDetails_KeyDown(object sender, KeyEventArgs e)
        {
            rbtnClick();
        }

        private void txtDebtorOpeningDebit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void mcbGroup_EnterKeyPressed(object sender, EventArgs e)
        {
            txtOpeningDebit.Focus();
            //if (mcbGroup.SeletedItem.ItemData[2].ToString() == "C")
            //{
            //}
        }


        private void UclAccount_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    rbtnDebtorDetails.Focus();
                    break;
            }
        }

        private void txtOpeningDebit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (txtOpeningDebit.Text.ToString() != "")
                            txtAddress1.Focus();
                        else
                            txtOpeningCredit.Focus();
                        break;
                    case Keys.Up:
                        if (mcbGroup.Enabled == true)
                            mcbGroup.Focus();
                        else
                            gbAccountType.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtOpeningCredit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtAddress1.Focus();
                        break;
                    case Keys.Left:
                        txtOpeningDebit.Focus();
                        break;
                    case Keys.Up:
                        txtOpeningDebit.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtContactPerson_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtTelephone.Focus();
                        break;
                    case Keys.Up:
                        txtAddress2.Focus();
                        break;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                    case Keys.Up:
                        txtContactPerson.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }



        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtRemark1.Focus();
                        break;
                    case Keys.Up:
                        txtTelephone.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtRemark1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtRemark2.Focus();
                        break;
                    case Keys.Up:
                        txtEmailId.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtRemark2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (rbtnCreditorDetails.Checked)
                            txtCreditorShortName.Focus();
                        else if (rbtnDebtorDetails.Checked)
                            txtNameAddress.Focus();
                        else if (rbtnBankDetails.Checked)
                            txtBankAccountNumber.Focus();
                        else
                            txtGeneralPAN.Focus();

                        break;
                    case Keys.Up:
                        txtRemark1.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtCreditorShortName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtCreditorDiscountOffered.Focus();
                    break;
                case Keys.Up:
                    txtRemark2.Focus();
                    break;
            }
        }

        private void txtCreditorDiscountOffered_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtCreditorVATTIN.Focus();
                    break;
                case Keys.Up:
                    txtCreditorShortName.Focus();
                    break;
            }
        }

        private void txtCreditorVATTIN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtCreditorDLN.Focus();
                    break;
                case Keys.Up:
                    txtCreditorDiscountOffered.Focus();
                    break;

            }
        }

        private void txtCreditorDLN_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtLessPercentInDebitNote.Focus();
                    break;
                case Keys.Up:
                    txtCreditorVATTIN.Focus();
                    break;
            }

           
        }

        private void txtLessPercentInDebitNote_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txtStatement15Days.Visible == true)
                        txtStatement15Days.Focus();
                    else
                        lvVisitDays.Focus();
                    break;
                case Keys.Up:
                    txtCreditorDLN.Focus();
                    break;
            }
        }

        private void txtStatement15Days_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    lvVisitDays.Focus();
                    break;
                case Keys.Up:
                    txtCreditorVATTIN.Focus();
                    break;
            }
        }

        private void mcbBank_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbBranch.Focus();
        }

        private void mcbBranch_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();
        }

        private void txtVATTIN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtDLN.Focus();
                    break;
                case Keys.Up:
                    txtvisit1.Focus();
                    break;
            }
        }

        private void txtDLN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                        lvVisitDays.Focus();
                    else if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                        txtTokenNumber.Focus();
                    break;
                case Keys.Up:
                    txtVATTIN.Focus();
                    break;
            }
        }

        private void txtTokenNumber_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtDebtorDiscountOffered.Focus();
                    break;
                case Keys.Up:
                    txtDLN.Focus();
                    break;
            }
        }
        private void txtDebtorDiscountOffered_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                gbTransactionType.Focus();
            else if (e.KeyCode == Keys.Up)
                txtTokenNumber.Focus();

        }

        private void txtGeneralPAN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtGeneralVATTIN.Focus();
                    break;
                case Keys.Up:
                    txtRemark2.Focus();
                    break;
            }
        }
        private void txtGeneralVATTIN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtGeneralVATTIN.Focus();
                    break;
                case Keys.Up:
                    txtGeneralPAN.Focus();
                    break;
            }
        }

        private void rbtnBankDetails_Click(object sender, EventArgs e)
        {
          //  if (_Mode == OperationMode.Edit)
                rbtnClick();
        }

        private void rbtnDebtorDetails_Click(object sender, EventArgs e)
        {
          //  if (_Mode == OperationMode.Edit)
                rbtnClick();
        }

        private void rbtnGeneralDetails_Click(object sender, EventArgs e)
        {
          //  if (_Mode == OperationMode.Edit)
                rbtnClick();
        }

        private void rbtnSaleDetails_Click(object sender, EventArgs e)
        {
          //  if (_Mode == OperationMode.Edit)
                rbtnClick();
        }

        private void rbtnPurchaseDetails_Click(object sender, EventArgs e)
        {
          //  if (_Mode == OperationMode.Edit)
                rbtnClick();
        }

        private void rbtnOCreditorDetails_Click(object sender, EventArgs e)
        {
          //  if (_Mode == OperationMode.Edit)
                rbtnClick();
        }

        private void rbtnODebtorDetails_Click(object sender, EventArgs e)
        {
         //   if (_Mode == OperationMode.Edit)
                rbtnClick();
        }

        private void rbtnCreditorDetails_Click(object sender, EventArgs e)
        {
          //  if (_Mode == OperationMode.Edit)
                rbtnClick();
        }
        private void txtvisit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtvisit2.Focus();
            else if (e.KeyCode == Keys.Up)
                mcbDoctor.Focus();
        }

        private void txtvisit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtvisit3.Focus();
        }

        private void txtvisit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtVATTIN.Focus();
        }
       
        #endregion

        #region ToolTip
        private void AddToolTip()
        {
            ttToolTip.SetToolTip(rbtnCreditorDetails, "Click and Press Enter ");
            ttToolTip.SetToolTip(rbtnBankDetails, "Click and Press Enter ");
            ttToolTip.SetToolTip(rbtnDebtorDetails, "Click and Press Enter ");
            ttToolTip.SetToolTip(rbtnGeneralDetails, "Click and Press Enter ");
            ttToolTip.SetToolTip(rbtnOCreditorDetails, "Click and Press Enter ");
            ttToolTip.SetToolTip(rbtnODebtorDetails, "Click and Press Enter ");
            ttToolTip.SetToolTip(rbtnPurchaseDetails, "Click and Press Enter ");
            ttToolTip.SetToolTip(rbtnSaleDetails, "Click and Press Enter ");
        }
        #endregion ToolTip

        private void mcbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                mcbBranch.Focus();
        }

     

     
       
        
    }
}
