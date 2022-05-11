//Description : This class contains all methods required for Shelf master. This is user control required for 
//              Add/Update/Delete shelf details


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
    public partial class UclShelf : BaseControl 
    {
        # region Declaration      
        private Shelf _Shelf;
        Hashtable htTableList;
        public int CurrentNumber = 0;
        # endregion

        # region Constructor
        public UclShelf()
        {
            InitializeComponent();
            _Shelf = new Shelf();
            SearchControl = new UclShelfSearch();
        } 
        # endregion Construtor

      
      
        # region IDetail Control
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Shelf.Initialise();
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
            headerLabel1.Text = "SHELF -> NEW";           
            AddToolTip();
            txtName.Focus();
            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            headerLabel1.Text = "SHELF -> EDIT";
            AddToolTip();
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
            headerLabel1.Text = "SHELF -> DELETE";
            ClearData();
            FilltxtName();
            txtName.Focus();
            panel1.Enabled = true;
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_Shelf.Id != null && _Shelf.Id != "")
            {
                retValue = _Shelf.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Shelf.DeleteDetails();
                    MessageBox.Show("Shelf information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            htTableList = General.GetTableListByCode("ShelfID", "ShelfCode", "Mastershelf");
            bool retValue = base.View();
            panel1.Enabled = true;
            ClearData();
            FilltxtName();
            headerLabel1.Text = "SHELF -> VIEW";
            MoveLast();
            return retValue;
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            CurrentNumber = 1;
            if (htTableList.Contains(CurrentNumber))
                _Shelf.Id = htTableList[CurrentNumber].ToString();
            FillSearchData(_Shelf.Id, "");

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
                    _Shelf.Id = htTableList[CurrentNumber].ToString();
                FillSearchData(_Shelf.Id, "");
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
                _Shelf.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber += 1;
            FillSearchData(_Shelf.Id, "");
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            CurrentNumber += 1;
            if (htTableList.Contains(CurrentNumber))
                _Shelf.Id = htTableList[CurrentNumber].ToString();
            else
                CurrentNumber -= 1;
            FillSearchData(_Shelf.Id, "");
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _Shelf.Code = txtName.Text.Trim();
            _Shelf.Name = txtName.Text.Trim();
            _Shelf.Description = txtShelfDescription.Text.Trim();
            if (_Mode == OperationMode.Edit)
                _Shelf.IFEdit = "Y";
            _Shelf.Validate();
            if (_Shelf.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {                   
                    _Shelf.CreatedBy = General.CurrentUser.Id;
                    _Shelf.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Shelf.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    //retValue = _Shelf.AddDetails();
                    //MessageBox.Show("Shelf information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //_SavedID = _Shelf.Id;               
                    //retValue = true;
                    _Shelf.IntID = _Shelf.AddDetails();
                    if (_Shelf.IntID > 0)
                    {
                        MessageBox.Show("Shelf information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SaveIntID = _Shelf.IntID;
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
                    _Shelf.ModifiedBy = General.CurrentUser.Id;
                    _Shelf.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Shelf.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Shelf.UpdateDetails();
                    MessageBox.Show("Shelf information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Shelf.Id;                  
                    retValue = true;
                }
            }
            else // Show Validation Messages
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Shelf.ValidationMessages)
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
                _Shelf.Id = ID;
                _Shelf.ReadDetailsByID();              
                txtName.Text = _Shelf.Code.Trim();
                txtShelfDescription.Text = _Shelf.Description.Trim();
                AddToolTip();
                headerLabel1.Text = "SHELF -> EDIT";
                txtName.Focus();
            }
            if (Mode == OperationMode.Delete || Mode == OperationMode.View)
                panel1.Enabled = false;
            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "ShelfID", "ShelfCode" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            Shelf _Shel = new Shelf();
            DataTable dtable = _Shel.GetOverviewData();
            txtName.FillData(dtable);
        }
        # endregion Idetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            if (keyPressed == Keys.C && modifier == Keys.Alt)
            {
                txtName.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.D && modifier == Keys.Alt)
            {
                txtShelfDescription.Focus();
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

        # region Other Private Methods
        public override bool IsDetailChanged()
        {
            bool retValue = false;           
            if (_Shelf.Code != txtName.Text.Trim())
                retValue = true;
            if (_Shelf.Description != txtShelfDescription.Text.Trim())
                retValue = true;
            return retValue;
        } 

        private void ClearControls()
        {
            txtName.Text = "";
            txtShelfDescription.Clear();
            txtName.Focus();
            tsBtnFifth.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSavenPrint.Visible = false;
        }
        #endregion OtherPrivate Methods

        # region Events
        private void txtShelfCode_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtShelfDescription.Focus();
                    break;
                case Keys.Down:
                    txtShelfDescription.Focus();
                    break;
                case Keys.Up:
                    txtShelfDescription.Focus();
                    break;
               
            }
        }

        private void txtShelfDescription_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) 
            {
                case Keys.Up:
                    txtName.Focus();
                    break;              
            }
            
        }
        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {

            FillSearchData(txtName.SelectedID,"");
            txtShelfDescription.Focus();

        }       

        # endregion Events

        #region tooltip
        private void AddToolTip()
        {
            ttShelf.SetToolTip(txtName, "A-Z,0-9,space only eg. A001,A023 etc.");
            ttShelf.SetToolTip(txtShelfDescription, "Fill the Location in Detail");


        }
        #endregion

       
    }
}
