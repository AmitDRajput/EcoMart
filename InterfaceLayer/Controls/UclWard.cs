//Description : This class contains all methods required for ward master. 
//              This is user control required for Add/Update/Delete ward details


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
    public partial class UclWard : BaseControl  
    {
        # region Declaration         
        private Ward _Ward;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        # endregion

        # region Constructor
        public UclWard()
        {
            InitializeComponent();
            _Ward = new Ward();
            SearchControl = new UclWardSearch();
        }
        # endregion

       
        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Ward.Initialise();
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
            AddToolTip();
            headerLabel1.Text = "WARD -> NEW";
            txtName.Focus();
            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            AddToolTip();
            headerLabel1.Text = "WARD -> EDIT";
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
            headerLabel1.Text = "WARD -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;          
            if (_Ward.Id != null && _Ward.Id != "")
            {
                retValue = _Ward.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Ward.DeleteDetails();
                    MessageBox.Show("Ward information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("WardID", "WardName", "MasterWard");
            bool retValue = base.View();
            ClearData();
            FilltxtName();
            headerLabel1.Text = "WARD -> VIEW";
            MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Ward.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Ward.Id, "");

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
                    _Ward.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Ward.Id, "");
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
                _Ward.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Ward.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Ward.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Ward.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _Ward.Name = txtName.Text;
            if (_Mode == OperationMode.Edit)
                _Ward.IFEdit = "Y";
            _Ward.Validate();
            if (_Ward.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _Ward.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _Ward.CreatedBy = General.CurrentUser.Id;
                    _Ward.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Ward.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Ward.AddDetails();
                    MessageBox.Show("Ward information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Ward.Id;
                    ClearControls();
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _Ward.ModifiedBy = General.CurrentUser.Id;
                    _Ward.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Ward.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Ward.UpdateDetails();
                    MessageBox.Show("Ward information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Ward.Id;
                    ClearControls();
                    retValue = true;
                }
            }
            else // Show Validation Wards
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Ward.ValidationMessages)
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
                _Ward.Id = ID;
                _Ward.ReadDetailsByID();
                txtName.Text = _Ward.Name;
                txtName.Focus();
            }

            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "WardID", "WardName" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            Ward _txt = new Ward();
            DataTable dtable = _txt.GetOverviewData();
            txtName.FillData(dtable);
        }

        # endregion Idetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
           // FilltxtName();
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
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
            if (_Ward.Name != null && _Ward.Name != txtName.Text)
                retValue = true;
            return retValue;
        }
        private void ClearControls()
        {
            txtName.Text = "";
            txtName.SelectedID = "";
            txtName.Focus();
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
            ttWard.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion     

    }
}
