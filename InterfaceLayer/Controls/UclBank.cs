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
    public partial class UclBank : BaseControl
    {
        #region Declaration
        private Bank _Bank;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        #endregion

        #region Constructor
        public UclBank()
        {
            try
            {
                InitializeComponent();
                _Bank = new Bank();
                SearchControl = new UclBankSearch();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion       

        # region IDetail Control
        public override void SetFocus()
        {
            this.txtName.Select();
            txtName.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _Bank.Initialise();
                ClearControls();
                txtName.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }
        public override bool Add()
        {
            bool retValue = false;
            try
            {
                retValue = base.Add();
                ClearData();
                pnlCenter.Enabled = true;
                FilltxtName();
                AddToolTip();
                headerLabel1.Text = "BANK -> NEW";
                txtName.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = false;
            try
            {
                retValue = base.Edit();
                ClearData();
                pnlCenter.Enabled = true;
                AddToolTip();
                headerLabel1.Text = "BANK -> EDIT";
                FilltxtName();
                txtName.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = false;
            try
            {
                retValue = base.Cancel();
                pnlCenter.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = false;
            try
            {
                retValue = base.Delete();
                headerLabel1.Text = "BANK -> DELETE";
                ClearData();
                FilltxtName();
                txtName.Focus();
                pnlCenter.Enabled = true;
                ProcessDelete();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            try
            {
                _Bank.Id = txtName.SelectedID;
                if (_Bank.Id != null && _Bank.Id != "")
                {
                    retValue = _Bank.CanBeDeleted();
                    if (retValue == true)
                    {
                        retValue = _Bank.DeleteDetails();
                        MessageBox.Show("Bank information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                pnlCenter.Enabled = true;
                ClearData();
                FilltxtName();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }
        public override bool View()
        {
            bool retValue = false;
            try
            {
                htTableList = General.GetTableListByCode("BankID", "BankName", "MasterBank");
                retValue = base.View();
                ClearData();
                FilltxtName();
                headerLabel1.Text = "BANK -> VIEW";
                MoveLast();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Bank.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Bank.Id, "");

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
                    _Bank.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Bank.Id, "");
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
                _Bank.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Bank.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Bank.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Bank.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;            
            try
            {
                System.Text.StringBuilder _errorMessage;
                _Bank.Name = txtName.Text;
                if (_Mode == OperationMode.Edit)
                    _Bank.IFEdit = "Y";
                _Bank.Validate();
                if (_Bank.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {                       
                        _Bank.CreatedBy = General.CurrentUser.Id;
                        _Bank.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Bank.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _Bank.IntID = _Bank.AddDetails();
                        if (_Bank.IntID > 0)
                        {
                             MessageBox.Show("Bank information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SaveIntID  = _Bank.IntID;
                            ClearControls();
                            FilltxtName();
                            retValue = true;
                        }
                        else
                        {
                            retValue = false;
                        }
                       
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        _Bank.ModifiedBy = General.CurrentUser.Id;
                        _Bank.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Bank.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Bank.UpdateDetails();
                        MessageBox.Show("Bank information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Bank.Id;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }
                }
                else 
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Bank.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public override bool FillSearchData(string  ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != string.Empty)
                {
                    _Bank.Id = ID;
                    _Bank.IntID = Convert.ToInt32(ID);
                    _Bank.ReadDetailsByID();
                    txtName.Text = _Bank.Name;
                    txtName.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

            return true;
        }
        public void FilltxtName()
        {
            try
            {
                txtName.SelectedID = null;
                txtName.SourceDataString = new string[2] { "BankID", "BankName" };
                txtName.ColumnWidth = new string[2] { "0", "300" };
                Bank _txt = new Bank();
                DataTable dtable = _txt.GetOverviewData();
                txtName.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        # endregion Idetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
            //if (closedControl is UclBank)
            //    FilltxtName();
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            try
            {
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
        # endregion IDetail Members

        #region other Private Methods


        public override bool IsDetailChanged()
        {
            bool retValue = false;
            try
            {
                if (_Bank.Name != null && _Bank.Name != txtName.Text)
                    retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        private void ClearControls()
        {
            try
            {
                txtName.Text = "";
                txtName.SelectedID = "";
                txtName.Focus();
                tsBtnFifth.Visible = false;
                tsBtnPrint.Visible = false;
                tsBtnSavenPrint.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Events

        private void txtName_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtName.SelectedID != null && txtName.SelectedID != "")
                {
                    _Bank.Id = txtName.SelectedID;
                    FillSearchData(txtName.SelectedID,"");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                Save();
        }

        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttBank.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion

       

    }
}
