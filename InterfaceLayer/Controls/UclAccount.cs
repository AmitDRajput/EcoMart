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
using System.Text.RegularExpressions;
using EcoMart.InterfaceLayer.CommonControls;
using System.Collections;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclAccount : BaseControl
    {
        #region Declaration
        private Account _Account;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        #endregion

        #region  Constructor
        public UclAccount()
        {
            try
            {
                InitializeComponent();
                _Account = new Account();
                SearchControl = new UclAccountSearch();
                htTableList = General.GetTableListByCode("AccountID", "AccName", "MasterAccount");
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
                //FillAutoAddress1();
                //FillAutoAddress2();
                headerLabel1.Text = "ACCOUNT -> NEW";
                HidePanels();
                FillGroupCombo();
                FillBankCombo();
                FillBranchCombo();
                FillAreaCombo();
                FillAccountType();
                GetAccTokenNumber();
                MaketsbtnVisibleFalse();
                txtName.Focus();
                AddToolTip();
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
                //FillAutoAddress1();
                //FillAutoAddress2();
                HidePanels();
                FillGroupCombo();
                FillBankCombo();
                FillBranchCombo();

                FillAreaCombo();
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
                //_Account.Id = txtName.SelectedID;
                
                if (_Account.Id != null && _Account.Id != "")
                {
                    retValue = _Account.CanBeDeleted();
                    if (cbAccountType.Text.ToString() == FixAccounts.AccTypeForCreditor && General.EcoMartLicense.ApplicationType != EcoMartLicenseLib.ApplicationTypes.EcoMart)
                        retValue = false;
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
                MoveLast();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Account.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Account.Id, "");

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
                    _Account.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Account.Id, "");
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
                _Account.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Account.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Account.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Account.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                if (cbAccountType.Text.ToString() == FixAccounts.AccTypeForCreditor)
                    _Account.AccCode = FixAccounts.AccCodeForCreditor;
                else if (cbAccountType.Text.ToString() == FixAccounts.AccTypeForDebtor)
                    _Account.AccCode = FixAccounts.AccCodeForDebtor;
                else if (cbAccountType.Text.ToString() == FixAccounts.AccTypeForBank)
                    _Account.AccCode = FixAccounts.AccCodeForBank;
                else if (cbAccountType.Text.ToString() == FixAccounts.AccTypeForGeneral)
                    _Account.AccCode = FixAccounts.AccCodeForGeneral;
                else if (cbAccountType.Text.ToString() == FixAccounts.AccTypeForOtherCreditor)
                    _Account.AccCode = FixAccounts.AccCodeForOtherCreditor;
                else if (cbAccountType.Text.ToString() == FixAccounts.AccTypeForOtherDebtor)
                    _Account.AccCode = FixAccounts.AccCodeForOtherDebtor;
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
                if (txtAutoAddress1.Text != null)
                    _Account.AccAddress1 = txtAutoAddress1.Text.ToString().Trim();
                if (txtAutoAddress2.Text != null)
                    _Account.AccAddress2 = txtAutoAddress2.Text.ToString().Trim();
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
                if (txtMobileNumberForSMS.Text != null && txtMobileNumberForSMS.Text.ToString() != string.Empty)
                    _Account.AccMobileNumberForSMS = txtMobileNumberForSMS.Text.ToString();
                if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                {
                    cbAccountType.Text = FixAccounts.AccTypeForDebtor;
                    cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.AccTypeForDebtor);
                    if (txtNameAddress.Text != null)
                        _Account.AccNameAddress = txtNameAddress.Text.Trim();
                    if (_Account.AccNameAddress.Length > 50)
                        _Account.AccNameAddress = _Account.AccNameAddress.Substring(0, 50);
                    if (mcbBank.SelectedID != null)
                        _Account.AccBankId = mcbBank.SelectedID;
                    if (mcbBranch.SelectedID != null)
                        _Account.AccBranchID = mcbBranch.SelectedID;
                    if (mcbArea.SelectedID != null)
                        _Account.AccAreaID = mcbArea.SelectedID;


                    if (txtVATTIN.Text != null)
                        _Account.AccVATTin = txtVATTIN.Text.Trim();
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
                    cbAccountType.Text = FixAccounts.AccTypeForCreditor;
                    cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.AccTypeForCreditor);
                    if (txtCreditorShortName.Text != null)
                        _Account.AccShortName = txtCreditorShortName.Text.ToString().Trim();
                    if (txtCreditorDiscountOffered.Text != null && txtCreditorDiscountOffered.Text != "")
                        _Account.AccDiscountOffered = Convert.ToDouble(txtCreditorDiscountOffered.Text.ToString());
                    if (txtCreditorVATTIN.Text != null)
                        _Account.AccVATTin = txtCreditorVATTIN.Text.ToString().Trim();
                    if (txtCreditorDLN.Text != null)
                        _Account.AccDLN = txtCreditorDLN.Text.ToString().Trim();

                    VisitDaysForDB = VisitDaysForDB.Trim();
                    _Account.AccCrVisitDays = VisitDaysForDB;

                }
                else if (_Account.AccCode == FixAccounts.AccCodeForBank)
                {
                    cbAccountType.Text = FixAccounts.AccTypeForBank;
                    cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.AccTypeForBank);
                    if (txtBankAccountNumber != null)
                        _Account.AccBankAccountNumber = txtBankAccountNumber.Text.ToString().Trim();
                }
                else
                {
                    if (txtGeneralPAN.Text != null)
                        _Account.AccPAN = txtGeneralPAN.Text.ToString().Trim();
                    if (txtGeneralVATTIN.Text != null)
                        _Account.AccVATTin = txtGeneralVATTIN.Text.ToString().Trim();
                }
                if (_Mode == OperationMode.Edit)
                    _Account.IFEdit = "Y";
                _Account.Validate();
                if (_Account.IsValid)
                {
                    //LockTable.LockTableForAccount();

                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        //_Account.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _Account.IntID = _Account.GetIntID();
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

                    FillAreaCombo();
                    FillAccountType();

                    //For Account type
                    if (_Account.AccCode == FixAccounts.AccCodeForDebtor)
                    {
                        cbAccountType.Text = FixAccounts.AccTypeForDebtor;
                        cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.AccTypeForDebtor);
                        mcbGroup.Enabled = false;
                        pnlDebtor.Visible = true;
                    }
                    if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                    {
                        cbAccountType.Enabled = true;
                        cbAccountType.Text = FixAccounts.AccTypeForCreditor;
                        cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.AccTypeForCreditor);
                        mcbGroup.Enabled = false;
                        pnlCreditor.Visible = true;
                    }
                    if (_Account.AccCode == FixAccounts.AccCodeForBank)
                    {
                        cbAccountType.Text = FixAccounts.AccTypeForBank;
                        cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.AccTypeForBank);
                        mcbGroup.Enabled = false;
                        pnlBank.Visible = true;
                        if (_Account.SetAsDefault == "Y")
                            cbSetAsDefault.Checked = true;
                        else
                            cbSetAsDefault.Checked = false;

                    }
                    if (_Account.AccCode != FixAccounts.AccCodeForDebtor && _Account.AccCode != FixAccounts.AccCodeForCreditor && _Account.AccCode != FixAccounts.AccCodeForBank)
                    {
                        cbAccountType.Text = FixAccounts.AccTypeForGeneral;
                        cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.AccTypeForGeneral);
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
                        txtAutoAddress1.Text = _Account.AccAddress1.Trim();
                    if (_Account.AccAddress2 != null)
                        txtAutoAddress2.Text = _Account.AccAddress2.Trim();
                    if (_Account.AccContactPerson != null)
                        txtContactPerson.Text = _Account.AccContactPerson.Trim();
                    if (_Account.AccTelephone != null)
                        txtTelephone.Text = _Account.AccTelephone.Trim();
                    if (_Account.AccMobileNumberForSMS != null)
                        txtMobileNumberForSMS.Text = _Account.AccMobileNumberForSMS;
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

                        if (_Account.AccVATTin != null)
                            txtVATTIN.Text = _Account.AccVATTin.Trim();

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

                        if (string.IsNullOrEmpty(Convert.ToString(_Account.AccDiscountOffered)) == false)
                            txtDebtorDiscountOffered.Text = _Account.AccDiscountOffered.ToString("#0.00");
                    }
                    else if (_Account.AccCode == FixAccounts.AccCodeForCreditor)
                    {
                        if (_Account.AccShortName != null)
                            txtCreditorShortName.Text = _Account.AccShortName.Trim();
                        txtCreditorDiscountOffered.Text = _Account.AccDiscountOffered.ToString("#0.00");
                        if (_Account.AccVATTin != null)
                            txtCreditorVATTIN.Text = _Account.AccVATTin.Trim();
                        if (_Account.AccDLN != null)
                            txtCreditorDLN.Text = _Account.AccDLN.Trim();

                    }
                    else if (_Account.AccCode == FixAccounts.AccCodeForBank)
                        txtBankAccountNumber.Text = _Account.AccBankAccountNumber.Trim();
                    else
                    {
                        if (_Account.AccVATTin != null)
                            txtGeneralVATTIN.Text = _Account.AccVATTin.Trim();
                        if (_Account.AccPAN != null)
                            txtGeneralPAN.Text = _Account.AccPAN.Trim();
                        FillGroupCombo();
                        mcbGroup.SelectedID = _Account.AccGroupID;
                        ShowHidePanels();
                    }

                    bool canacccodechange = _Account.CanBeDeleted();
                    if (!canacccodechange)
                    {
                        cbAccountType.Enabled = false;
                    }
                    else
                        cbAccountType.Enabled = true;
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

        public override void ReFillData(Control closedControl)
        {
            try
            {
                if (closedControl is UclBank)
                    FillBankCombo();
                else if (closedControl is UclBranch)
                    FillBranchCombo();
                else if (closedControl is UclGroup)
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
                    txtAutoAddress1.Focus();
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
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtAutoAddress1.Text = "";
                txtAutoAddress2.Text = "";
                txtContactPerson.Clear();
                txtMobileNumberForSMS.Text = "";
                txtEmailId.Clear();
                mcbGroup.SelectedID = "";
                txtOpeningCredit.Clear();
                txtOpeningDebit.Clear();
                txtRemark1.Text = "";
                txtRemark2.Text = "";
                txtTelephone.Clear();
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
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
                if (cbAccountType.Text == FixAccounts.AccTypeForDebtor)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForDebtor;
                    _Account.AccGroupID = FixAccounts.GroupCodeForDebtor.ToString();
                    FillGroupCombo();
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForDebtor.ToString();
                    mcbGroup.Enabled = false;
                    txtOpeningDebit.Focus();
                }
                else if (cbAccountType.Text == FixAccounts.AccTypeForCreditor)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForCreditor;
                    _Account.AccGroupID = FixAccounts.GroupCodeForCreditor.ToString();
                    FillGroupCombo();
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForCreditor.ToString();
                    mcbGroup.Enabled = false;
                    txtOpeningDebit.Focus();
                }
                else if (cbAccountType.Text == FixAccounts.AccTypeForBank)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForBank;
                    _Account.AccGroupID = FixAccounts.GroupCodeForBank.ToString();
                    FillGroupCombo();
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForBank.ToString();
                    mcbGroup.Enabled = false;
                    txtOpeningDebit.Focus();
                }
                else if (cbAccountType.Text == FixAccounts.AccTypeForGeneral)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForGeneral;
                    _Account.AccGroupID = "";
                    mcbGroup.SelectedID = "";
                    FillGroupCombo();
                    mcbGroup.Enabled = true;
                    mcbGroup.Focus();
                }
                else if (cbAccountType.Text == FixAccounts.AccTypeForSale)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForSale;
                    mcbGroup.SelectedID = null;
                    FillGroupCombo();
                    mcbGroup.Enabled = true;
                    mcbGroup.Focus();
                }
                else if (cbAccountType.Text == FixAccounts.AccTypeForPurchase)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForPurchase;
                    mcbGroup.SelectedID = null;
                    FillGroupCombo();
                    mcbGroup.Enabled = true;
                    mcbGroup.Enabled = true;
                    txtOpeningDebit.Focus();
                }
                else if (cbAccountType.Text == FixAccounts.AccTypeForOtherCreditor)
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForOtherCreditor;
                    _Account.AccGroupID = FixAccounts.GroupCodeForOtherCreditor.ToString();
                    mcbGroup.SelectedID = null;
                    FillGroupCombo();
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForOtherCreditor.ToString();
                    mcbGroup.Enabled = false;
                    txtOpeningDebit.Focus();
                }
                else
                {
                    mcbGroup.Enabled = true;
                    _Account.AccCode = FixAccounts.AccCodeForOtherDebtor;
                    _Account.AccGroupID = FixAccounts.GroupCodeForOtherDebtor.ToString();
                    mcbGroup.SelectedID = null;
                    FillGroupCombo();
                    mcbGroup.SelectedID = null;
                    mcbGroup.SelectedID = FixAccounts.GroupCodeForOtherDebtor.ToString();
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
                // txtName.Focus();
                if (cbAccountType.Text == FixAccounts.AccTypeForDebtor)
                {
                    pnlCreditor.Visible = false;
                    pnlBank.Visible = false;
                    pnlGeneral.Visible = false;
                    pnlDebtor.Visible = true;
                    pnlDebtor.BringToFront();
                }
                else if (cbAccountType.Text == FixAccounts.AccTypeForCreditor)
                {
                    pnlDebtor.Visible = false;
                    pnlBank.Visible = false;
                    pnlGeneral.Visible = false;
                    pnlCreditor.Visible = true;
                    pnlCreditor.BringToFront();
                    mcbGroup.SelectedID = "31";

                }
                else if (cbAccountType.Text == FixAccounts.AccTypeForBank)
                {
                    pnlDebtor.Visible = false;
                    pnlCreditor.Visible = false;
                    pnlGeneral.Visible = false;
                    pnlBank.Visible = true;
                    pnlBank.BringToFront();
                }
                else
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

            if (General.EcoMartLicense.ApplicationType == EcoMartLicenseLib.ApplicationTypes.EcoMart )
                cbAccountType.Items.Add(FixAccounts.AccTypeForCreditor);

            cbAccountType.Items.Add(FixAccounts.AccTypeForDebtor);
            cbAccountType.Items.Add(FixAccounts.AccTypeForBank);
            cbAccountType.Items.Add(FixAccounts.AccTypeForGeneral);
            cbAccountType.Items.Add(FixAccounts.AccTypeForOtherCreditor);
            cbAccountType.Items.Add(FixAccounts.AccTypeForOtherDebtor);
            cbAccountType.Items.Add(FixAccounts.AccTypeForPurchase);
            cbAccountType.Items.Add(FixAccounts.AccTypeForSale);

            cbAccountType.Text = FixAccounts.AccTypeForDebtor;
            cbAccountType.SelectedIndex = cbAccountType.Items.IndexOf(FixAccounts.AccTypeForDebtor);
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

        private void FillAreaCombo()
        {
            try
            {
                mcbArea.SelectedID = null;
                mcbArea.SourceDataString = new string[2] { "AreaID", "AreaName" };
                mcbArea.ColumnWidth = new string[2] { "0", "300" };
                mcbArea.ValueColumnNo = 0;
                mcbArea.UserControlToShow = new UclArea();
                Area _txt = new Area();
                DataTable dtable = _txt.GetOverviewData();
                mcbArea.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillAutoAddress1()  // [ansuman][28.12.2016]
        {
            try
            {
                txtAutoAddress1.SelectedID = null;
                txtAutoAddress1.SourceDataString = new string[2] { "AccAddress1", "AccAddress2" };
                txtAutoAddress1.ColumnWidth = new string[2] { "400", "0" };
                txtAutoAddress1.DisplayColumnNo = 0;
                txtAutoAddress1.ValueColumnNo = 0;
                Account filltxt = new Account();
                DataTable dtable = filltxt.GetOverviewAddress1();
                txtAutoAddress1.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillAutoAddress2()  // [ansuman][28.12.2016]
        {
            try
            {
                txtAutoAddress2.SelectedID = null;
                txtAutoAddress2.SourceDataString = new string[1] { "AccAddress2" };
                txtAutoAddress2.ColumnWidth = new string[1] { "400" };
                txtAutoAddress2.DisplayColumnNo = 0;
                txtAutoAddress2.ValueColumnNo = 0;
                Account filltxt = new Account();
                DataTable dtable = filltxt.GetOverviewAddress2();
                txtAutoAddress2.FillData(dtable);
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
            mcbArea.Focus();

        }
        private void mcbArea_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbArea.SelectedID;
            FillAreaCombo();
            mcbArea.SelectedID = selectedId;
        }

        private void mcbArea_KeyDown(object sender, KeyEventArgs e)
        {

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
                string myID = "";
                if (txtName.SelectedID != null)
                    myID = txtName.SelectedID;
                ClearData();
                txtName.SelectedID = myID;
                string Name = txtName.Text;
                if (_Account.AccName != "" && _Account.AccName != txtName.Text.ToString())
                {
                    txtName.SelectedID = "";
                }
                if (string.IsNullOrEmpty(myID) == true && _Mode == OperationMode.Add && string.IsNullOrEmpty(Name) == false)
                {
                    string ShortName = string.Empty;
                    if (Name.Length > 3)
                        ShortName = Name.Substring(0, 3);
                    else
                        ShortName = Name.Substring(0, Name.Length);
                    txtCreditorShortName.Text = ShortName;
                }
                TxtNameEnterKeyPressed();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void TxtNameEnterKeyPressed()
        {
            try
            {
                if (txtName.SelectedID != "")
                    FillSearchData(txtName.SelectedID, "");
                txtNameAddress.Text = txtName.Text.ToString().Trim() + " " + txtAutoAddress1.Text.ToString().Trim();
                if (mcbGroup.SelectedID == null || mcbGroup.SelectedID == "")
                {
                    cbAccountType.Enabled = true;
                    cbAccountType.Focus();
                }
                else
                {
                    if (_Mode == OperationMode.Edit)
                        txtOpeningDebit.Focus();
                    else
                        txtName.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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


        //private void UclAccount_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.F2:
        //            rbtnDebtorDetails.Focus();
        //            break;
        //    }
        //}

        private void txtOpeningDebit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (txtOpeningDebit.Text.ToString() != "" && Convert.ToDouble(txtOpeningDebit.Text.ToString()) > 0)
                            txtAutoAddress1.Focus();
                        else
                            txtOpeningCredit.Focus();
                        break;
                    case Keys.Up:
                        if (mcbGroup.Enabled == true)
                            mcbGroup.Focus();
                        else
                            cbAccountType.Focus();
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
                        txtAutoAddress1.Focus();
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
                        txtAutoAddress2.Focus();
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
                        txtMobileNumberForSMS.Focus();
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
                        if (cbAccountType.Text == FixAccounts.AccTypeForCreditor)
                            txtCreditorShortName.Focus();
                        // txtCreditorShortName.Focus();
                        else if (cbAccountType.Text == FixAccounts.AccTypeForDebtor)
                            mcbBank.Focus();
                        //    txtNameAddress.Focus();
                        else if (cbAccountType.Text == FixAccounts.AccTypeForBank)
                            txtAccountNumber.Focus();
                        // txtBankAccountNumber.Focus();
                        //else
                        //    txtMobileNumberForSMS.Focus();
                        // txtGeneralPAN.Focus();

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
        private void txtMobileNumberForSMS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        txtEmailId.Focus();
                        //if (cbAccountType.Text == FixAccounts.AccTypeForCreditor)                            
                        // txtCreditorShortName.Focus();
                        //else if (cbAccountType.Text == FixAccounts.AccTypeForDebtor)                            
                        //    txtNameAddress.Focus();
                        //else if (cbAccountType.Text == FixAccounts.AccTypeForBank)                            
                        // txtBankAccountNumber.Focus();
                        //else                           
                        // txtGeneralPAN.Focus();
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

        private void mcbBank_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbBranch.Focus();
        }

        private void mcbBranch_EnterKeyPressed(object sender, EventArgs e)
        {

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

        private void txtvisit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtVATTIN.Focus();
        }

        #endregion

        #region ToolTip
        private void AddToolTip()
        {
            //ttToolTip.SetToolTip(rbtnCreditorDetails, "Click and Press Enter ");
            //ttToolTip.SetToolTip(rbtnBankDetails, "Click and Press Enter ");
            //ttToolTip.SetToolTip(rbtnDebtorDetails, "Click and Press Enter ");
            //ttToolTip.SetToolTip(rbtnGeneralDetails, "Click and Press Enter ");
            //ttToolTip.SetToolTip(rbtnOCreditorDetails, "Click and Press Enter ");
            //ttToolTip.SetToolTip(rbtnODebtorDetails, "Click and Press Enter ");
            //ttToolTip.SetToolTip(rbtnPurchaseDetails, "Click and Press Enter ");
            //ttToolTip.SetToolTip(rbtnSaleDetails, "Click and Press Enter ");
        }
        #endregion ToolTip

        #region UIEventes

        private void mcbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                mcbBranch.Focus();
        }

        private void cbAccountType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetAccountType();
                ShowHidePanels();
            }
        }

        private void UclAccount_Load(object sender, EventArgs e)
        {
            MaketsbtnVisibleFalse();
            FillAutoAddress1();
            FillAutoAddress2();

        }

        private void MaketsbtnVisibleFalse()
        {

            tsBtnFifth.Visible = false;
            tsBtnSavenPrint.Visible = false;
            //tsBtnSearch.Visible = false;
        }

        private void txtName_SeletectIndexChanged(object sender, EventArgs e)
        {

            string selectedindex = "";
            selectedindex = txtName.SelectedID;
            if (selectedindex != null && selectedindex != string.Empty)
                TxtNameEnterKeyPressed();
        }

        private void mcbBank_UpArrowPressed(object sender, EventArgs e)
        {
            txtRemark2.Focus();
        }

        private void txtAutoAddress1_EnterKeyPressed(object sender, EventArgs e)
        {
            if (txtAutoAddress1.SeletedItem != null)
                txtAutoAddress2.Text = txtAutoAddress1.SeletedItem.ItemData[1].ToString();
            txtAutoAddress2.Focus();
        }

        private void txtAutoAddress2_EnterKeyPressed(object sender, EventArgs e)
        {
            txtContactPerson.Focus();
        }

        private void txtAutoAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    txtContactPerson.Focus();
                    break;
                case Keys.Up:
                    txtAutoAddress1.Focus();
                    break;
            }
        }

        private void txtAutoAddress2_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtAutoAddress1.Focus();
        }

        private void txtAutoAddress1_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtOpeningCredit.Focus();
        }
        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != ',') && (e.KeyChar != '/'))
            {
                e.Handled = true;
            }
        }
        private void lvVisitDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
        }
        private void txtContactPerson_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        #endregion UIEventes

    }
}
