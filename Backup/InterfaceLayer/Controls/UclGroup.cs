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


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclGroup : BaseControl
    {
        # region Declaration      
        private Groupac _Group; 
        # endregion Declaration

        # region constructor
        public UclGroup()
        {
            InitializeComponent();
            _Group = new Groupac();
            SearchControl = new UclGroupSearch();
        }        
        # endregion constructor
                
        # region IDetail Control 
        public override void SetFocus()
        {
            txtName.Focus();
        }
        public override bool ClearData()
        {
            _Group.Initialise();
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
            FillUnderGroupCombo();
            AddToolTip();
            headerLabel1.Text = "GROUP -> NEW";
            txtName.Focus();
            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            panel1.Enabled = true;
            AddToolTip();
            FilltxtName();            
            FillUnderGroupCombo();
            headerLabel1.Text = "GROUP -> EDIT";
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
            //  headerLabel1.Text = "DOCTOR -> DELETE";
            ClearData();
            FilltxtName();
            FillUnderGroupCombo();
            txtName.Focus();
            panel1.Enabled = true;
            return true;  
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            //_Group.Id = txtName.SelectedID;
            if (_Group.Id != null && _Group.Id != "")
            {
                retValue = _Group.CanBeDeleted();
                if (retValue == true)
                {
                    retValue = _Group.DeleteDetails();
                    MessageBox.Show("Group Information has been Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            bool retValue = base.View();
            panel1.Enabled = true;
            ClearData();
            FilltxtName();
            headerLabel1.Text = "GROUP -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            _Group.Name = txtName.Text.Trim();
            _Group.UnderGroupId = mcbUnderGroup.SelectedID;
            _Group.UnderGroupIDParentID = Convert.ToInt32(mcbUnderGroup.SeletedItem.ItemData[4].ToString());
            if (_Mode == OperationMode.Edit)
                _Group.IFEdit = "Y";
            _Group.Validate();
            if (_Group.IsValid)
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    _Group.GroupIDInteger = _Group.GetGroupIDInteger();
                  //  _Group.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _Group.CreatedBy = General.CurrentUser.Id;
                    _Group.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Group.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Group.AddDetails();
                    MessageBox.Show("Group information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Group.Id;               
                    retValue = true;
                }
                else if (_Mode == OperationMode.Edit)
                {
                    _Group.ModifiedBy = General.CurrentUser.Id;
                    _Group.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _Group.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    retValue = _Group.UpdateDetails();
                    MessageBox.Show("Group information has been updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _SavedID = _Group.Id;
                    retValue = true;
                }
            }
            else // Show Validation Messages
            {
                _errorMessage = new System.Text.StringBuilder();
                _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                foreach (string _message in _Group.ValidationMessages)
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
                _Group.Id = ID;
                _Group.ReadDetailsByID();
                if (_Group.IfMainGroup.ToUpper() != "Y")
                {
                    FillUnderGroupCombo();
                    txtName.Text = _Group.Name;
                    mcbUnderGroup.SelectedID = _Group.UnderGroupId;
                    
                }
            }
            if (Mode == OperationMode.Delete || Mode == OperationMode.View)
                panel1.Enabled = false;
            return true;
        }

        public void FilltxtName()
        {
            txtName.SelectedID = null;
            txtName.SourceDataString = new string[2] { "GroupID", "GroupName" };
            txtName.ColumnWidth = new string[2] { "0", "300" };
            Groupac _Group = new Groupac();
            DataTable dtable = _Group.GetOverviewData();
            txtName.FillData(dtable);
        }

        # endregion Idetail Control

        #region IDetail Members

        public override void ReFillData()
        {
            FilltxtName();
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

        # region OtherPrivate Methods
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        private void FillUnderGroupCombo()
        {
            mcbUnderGroup.SelectedID = null;
            mcbUnderGroup.SourceDataString = new string[5] { "GroupID", "GroupName", "IFFIX", "IFMainGroup" , "UnderGroupIDParentID"};
            mcbUnderGroup.ColumnWidth =  new string[5] {"0" , "200", "0" ,"0","0" };
            mcbUnderGroup.ValueColumnNo = 0;
        //    mcbUnderGroup.UserControlToShow = new UclGroup();
            Groupac _Group = new Groupac();
            DataTable dtable = _Group.GetOverviewDataALL();
            mcbUnderGroup.FillData(dtable);
        }

        public void ClearControls()
        {
            txtName.Text = "";
            txtName.SelectedID = "";
            mcbUnderGroup.SelectedID = "";
        }
        # endregion OtherPrivate Methods
       

        # region Events
        
       private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    mcbUnderGroup.Focus();
                    break;
            }
        }       
        # endregion

        #region tooltip
        private void AddToolTip()
        {
            ttGroup.SetToolTip(txtName, "A-Z,0-9,space only");
            ttGroup.SetToolTip(mcbUnderGroup, "Shift Tab to GOTO Name");


        }
        #endregion             

        private void mcbUnderGroup_EnterKeyPressed(object sender, EventArgs e)
        {

        }
        private void txtName_EnterKeyPressed(object sender, EventArgs e)
        {

            FillSearchData(txtName.SelectedID,"");
            mcbUnderGroup.Focus();
        }

    }
}
