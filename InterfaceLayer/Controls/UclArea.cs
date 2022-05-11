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
    public partial class UclArea : BaseControl
    {
        #region Declaration
        private Area _Area;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        #endregion

        #region Constructor
        public UclArea()
        {
            try
            {
                InitializeComponent();
                _Area = new Area();
                SearchControl = new UclAreaSearch();
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
            txtName.Focus();
        }
        public override bool ClearData()
        {
            try
            {
                _Area.Initialise();
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
                headerLabel1.Text = "AREA -> NEW";
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
                headerLabel1.Text = "AREA -> EDIT";
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
                headerLabel1.Text = "AREA -> DELETE";
                ClearData();
                FilltxtName();
                txtName.Focus();
                pnlCenter.Enabled = true;
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
                _Area.Id = txtName.SelectedID;
                if (_Area.Id != null && _Area.Id != "")
                {
                    retValue = _Area.CanBeDeleted();
                    if (retValue == true)
                    {
                        retValue = _Area.DeleteDetails();
                        MessageBox.Show("Area information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("AreaID", "AreaName", "MasterArea");
            bool retValue = false;
            try
            {
                retValue = base.View();
                ClearData();
                FilltxtName();
                headerLabel1.Text = "AREA -> VIEW";
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
                _Area.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Area.Id, "");

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
                    _Area.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Area.Id, "");
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
                _Area.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Area.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Area.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Area.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;
                _Area.Name = txtName.Text;
                if (_Mode == OperationMode.Edit)
                    _Area.IFEdit = "Y";
                _Area.Validate();
                if (_Area.IsValid)
                {
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        _Area.IntID  = _Area.GetIntID(); /* Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/
                        _Area.CreatedBy = General.CurrentUser.Id;
                        _Area.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Area.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        //retValue = _Area.AddDetails();
                        //MessageBox.Show("Area information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _Area.IntID = _Area.AddDetails();
                        if (_Area.IntID > 0)
                        {
                            MessageBox.Show("Area information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SaveIntID = _Area.IntID;
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
                        _Area.ModifiedBy = General.CurrentUser.Id;
                        _Area.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _Area.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _Area.UpdateDetails();
                        MessageBox.Show("Area information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _Area.Id;
                        ClearControls();
                        FilltxtName();
                        retValue = true;
                    }                    
                }
                else // Show Validation Messages
                {
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _Area.ValidationMessages)
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
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _Area.Id = ID;
                    _Area.ReadDetailsByID();
                    txtName.Text = _Area.Name;
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
                txtName.SourceDataString = new string[2] { "AreaID", "AreaName" };
                txtName.ColumnWidth = new string[2] { "0", "300" };
                Area _txt = new Area();
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
            //if (closedControl is UclArea)
            //    FilltxtName();
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
            try
            {
                if (_Area.Name != null && _Area.Name != txtName.Text)
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
                    _Area.Id = txtName.SelectedID;
                    FillSearchData(txtName.SelectedID,"");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //private void txtName_EnterKeyPressed(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (_Mode == OperationMode.Add)
        //            Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

       
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttArea.SetToolTip(txtName, "A-Z,0-9,space only");
        }
        #endregion

        

       
    }
}
